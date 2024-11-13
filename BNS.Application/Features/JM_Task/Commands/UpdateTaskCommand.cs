using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using System.Collections.Generic;
using BNS.Domain.Interface;
using BNS.Service.Implement.BaseImplement;
using Newtonsoft.Json;

namespace BNS.Service.Features
{
    public class UpdateTaskCommand : UpdateRequestHandler<UpdateTaskRequest, JM_Task>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachedFileService _attachedFileService;

        public UpdateTaskCommand(IMapper mapper,
            IUnitOfWork unitOfWork,
            IAttachedFileService attachedFileService) : base(unitOfWork, mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _attachedFileService = attachedFileService;
        }
        public override async Task<ApiResult<Guid>> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<JM_Task>().AsNoTracking()
                .Include(s => s.TaskType)
                .ThenInclude(s => s.Template).ThenInclude(s => s.TemplateDetails)
                .Include(s => s.TaskCustomColumnValues)
                .Include(s => s.TaskTags)
                .Include(s => s.Childs)
                .Where(s => s.Id == request.Id).FirstOrDefaultAsync();
            if (dataCheck == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = LocalizedBackendMessages.MSG_ObjectNotExists;
                return response;
            }

            var assignUserIds = new List<Guid>();
            if (dataCheck.AssignUserId.HasValue)
            {
                assignUserIds.Add(dataCheck.AssignUserId.Value);
            }

            if (request.UserId != dataCheck.ReporterId && (dataCheck.AssignUserId != request.UserId && !assignUserIds.Contains(request.UserId)))
            {
                //if (dataCheck.TaskTypeId != request.DefaultData.TaskTypeId)
                //{
                response.errorCode = EErrorCode.Failed.ToString();
                response.title = LocalizedBackendMessages.Message.MSG_NotPermissionEdit;
                return response;
                //}
            }
            UpdateEntity(dataCheck, request.ChangeFields.DefaultData, request.UserId);
            if (request.ChangeFields.DefaultData != null && request.ChangeFields.DefaultData.Count > 0)
            {
                var usersAssignIds = request.ChangeFields.DefaultData.Where(s => s.Key.Equals("usersAssignIds")).FirstOrDefault();
                var tags = request.ChangeFields.DefaultData.Where(s => s.Key.Equals("tags")).FirstOrDefault();
                var childIds = request.ChangeFields.DefaultData.Where(s => s.Key.Equals("childIds")).FirstOrDefault();

                #region Assign user

                if (usersAssignIds != null)
                {
                    var value = JsonConvert.DeserializeObject<ChangeFieldTransferItem<Guid>>(usersAssignIds.Value.ToString());
                    if (value.DeleteValues != null && value.DeleteValues.Count > 0)
                    {
                        if (dataCheck.AssignUserId.HasValue && value.DeleteValues.Contains(dataCheck.AssignUserId.Value))
                        {
                            dataCheck.AssignUserId = null;
                        }
                    }
                    if (value.AddValues != null && value.AddValues.Count > 0)
                    {
                        var addValues = value.AddValues;
                        if (!dataCheck.AssignUserId.HasValue && addValues.Count == 1)
                        {
                            dataCheck.AssignUserId = addValues[0];
                        }
                        else
                        {
                            addValues.ForEach(s => _unitOfWork.Repository<JM_TaskUser>().Add(new JM_TaskUser
                            {
                                TaskId = dataCheck.Id,
                                UserId = s,
                                CompanyId = request.CompanyId,
                                CreatedUserId = request.UserId,
                            }));
                        }
                    }
                }

                #endregion

                #region Tags
                if (tags != null)
                {
                    var tagOld = dataCheck.TaskTags?.ToList();
                    var value = JsonConvert.DeserializeObject<List<TagItem>>(tags.Value.ToString());
                    if (value != null && value.Count > 0)
                    {
                        var deleteIds = value.Where(s => s.RowStatus == ERowStatus.Delete).Select(s => s.Id).ToList();
                        var addNewDataRows = value.Where(s => s.IsAddNew == true).ToList();
                        var addNewIds = value.Where(s => s.IsAddNew != true).ToList();
                        if (deleteIds.Count > 0)
                        {
                            var datasDelete = dataCheck.TaskTags?.Where(s => deleteIds.Contains(s.TagId));
                            _unitOfWork.Repository<JM_TaskTag>().RemoveRange(datasDelete);
                        }
                        foreach (var item in addNewDataRows)
                        {
                            _unitOfWork.Repository<JM_Tag>().Add(new JM_Tag
                            {
                                CompanyId = request.CompanyId,
                                CreatedUserId = request.UserId,
                                Id = item.Id.Value,
                                Name = item.Name,
                            });

                            _unitOfWork.Repository<JM_TaskTag>().Add(new JM_TaskTag
                            {
                                TagId = item.Id.Value,
                                TaskId = dataCheck.Id,
                                CompanyId = request.CompanyId,
                                CreatedUserId = request.UserId,
                            });
                        }

                        foreach (var item in addNewIds)
                        {
                            _unitOfWork.Repository<JM_TaskTag>().Add(new JM_TaskTag
                            {
                                TagId = item.Id.Value,
                                TaskId = dataCheck.Id,
                                CompanyId = request.CompanyId,
                                CreatedUserId = request.UserId,
                            });
                        }
                    }
                }
                #endregion

                #region Task Childs
                if (childIds != null)
                {
                    var value = JsonConvert.DeserializeObject<ChangeFieldTransferItem<Guid>>(childIds.Value.ToString());
                    if (value.DeleteValues != null && value.DeleteValues.Count > 0)
                    {
                        var taskChildDeletes = dataCheck.Childs.Where(s => value.DeleteValues.Contains(s.Id)).ToList();
                        foreach (var item in taskChildDeletes)
                        {
                            item.ParentId = null;
                            item.JM_TaskParent = null;
                            item.UpdatedDate = DateTime.UtcNow;
                            item.UpdatedUserId = request.UserId;
                        }
                        _unitOfWork.Repository<JM_Task>().UpdateRange(taskChildDeletes);
                        await _unitOfWork.SaveChangesAsync();
                    }
                }
                #endregion
            }

            #region Dynamic task data

            var templateDetails = dataCheck.TaskType.Template?.TemplateDetails.ToList();

            if (templateDetails != null && templateDetails.Count > 0)
            {
                var dataDynamics = request.ChangeFields.DynamicData;
                if (dataDynamics != null && dataDynamics.Count > 0)
                {
                    var taskCustomColumnValues = dataCheck.TaskCustomColumnValues != null ? dataCheck.TaskCustomColumnValues.ToList() : new List<JM_TaskCustomColumnValue>();
                    foreach (var value in dataDynamics)
                    {
                        var templateDetail = templateDetails.Where(s => s.Id.Equals(Guid.Parse(value.Key))).FirstOrDefault();
                        if (templateDetail != null)
                        {
                            var taskCustomColumnValue = taskCustomColumnValues.Where(s => s.CustomColumnId == templateDetail.CustomColumnId).FirstOrDefault();
                            if (taskCustomColumnValue != null)
                            {
                                taskCustomColumnValue.Value = value.Value.ToString();
                                taskCustomColumnValue.UpdatedDate = DateTime.UtcNow;
                                taskCustomColumnValue.UpdatedUserId = request.UserId;
                                _unitOfWork.Repository<JM_TaskCustomColumnValue>().Update(taskCustomColumnValue);
                            }
                            else
                            {
                                var taskCustomColumns = new JM_TaskCustomColumnValue
                                {
                                    TaskId = dataCheck.Id,
                                    CustomColumnId = templateDetail.CustomColumnId.Value,
                                    TemplateDetailId = templateDetail.Id,
                                    CreatedUserId = request.UserId,
                                    CompanyId = request.CompanyId,
                                    Value = value.Value.ToString(),
                                };
                                _unitOfWork.Repository<JM_TaskCustomColumnValue>().Add(taskCustomColumns);
                            }
                        }
                    }
                }
            }

            #endregion


            //#region Task child delete
            //if (request.DefaultData.TaskChildDelete != null && request.DefaultData.TaskChildDelete.Count > 0)
            //{
            //    var taskChildDeletes = await _unitOfWork.Repository<JM_Task>().Where(s => request.DefaultData.TaskChildDelete.Contains(s.Id)).ToListAsync();
            //    foreach (var item in taskChildDeletes)
            //    {
            //        item.ParentId = null;
            //        item.UpdatedDate = DateTime.UtcNow;
            //        item.UpdatedUserId = request.UserId;
            //        _unitOfWork.Repository<JM_Task>().Update(item);
            //    }
            //}
            //#endregion

            //#region Task child add
            //if (request.DefaultData.TaskChild != null && request.DefaultData.TaskChild.Count > 0)
            //{
            //    var taskChildDeletes = await _unitOfWork.Repository<JM_Task>().Where(s => request.DefaultData.TaskChild.Contains(s.Id)).ToListAsync();
            //    foreach (var item in taskChildDeletes)
            //    {
            //        item.ParentId = dataCheck.Id;
            //        item.UpdatedDate = DateTime.UtcNow;
            //        item.UpdatedUserId = request.UserId;
            //        _unitOfWork.Repository<JM_Task>().Update(item);
            //    }
            //}
            //#endregion

            //#region Files
            //if (request.DefaultData.Files != null && request.DefaultData.Files.Count > 0)
            //{
            //    var fileAddNew = request.DefaultData.Files.Where(s => s.IsAddNew).Select(s => new CreateAttachedFilesRequest
            //    {
            //        EntityId = dataCheck.Id,
            //        CompanyId = request.CompanyId,
            //        Url = s.Url,
            //        UserId = request.UserId,
            //        File = s.File,
            //    }).ToList();
            //    await _attachedFileService.AddAttachedFiles(fileAddNew);

            //    var fileDelete = request.DefaultData.Files.Where(s => s.IsDelete).Select(s => s.Id).ToList();
            //    if (fileDelete != null && fileDelete.Count > 0)
            //    {
            //        await _attachedFileService.RemoveAttachedFiles(fileDelete);
            //    }
            //}
            //#endregion

            _unitOfWork.Repository<JM_Task>().Update(dataCheck);
            await _unitOfWork.SaveChangesAsync();
            response.data = dataCheck.Id;
            return response;
        }

    }

}
