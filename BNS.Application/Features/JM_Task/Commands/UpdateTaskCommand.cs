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

namespace BNS.Service.Features
{
    public class UpdateTaskCommand : IRequestHandler<UpdateTaskRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachedFileService _attachedFileService;

        public UpdateTaskCommand(IMapper mapper,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IAttachedFileService attachedFileService)
        {
            _mapper = mapper;
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
            _attachedFileService = attachedFileService;
        }
        public async Task<ApiResult<Guid>> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<JM_Task>()
                .Include(s => s.TaskType).ThenInclude(s => s.Template).ThenInclude(s => s.TemplateDetails)
                .Include(s => s.TaskCustomColumnValues)
                .Include(s => s.TaskTags)
                .Where(s => s.Id == request.Id).FirstOrDefaultAsync();
            if (dataCheck == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }

            if (request.UserId != dataCheck.ReporterId)
            {
                if (dataCheck.TaskTypeId != request.DefaultData.TaskTypeId)
                {
                    response.errorCode = EErrorCode.Failed.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.Message.MSG_NotPermissionEdit];
                    return response;
                }
            }

            _mapper.Map(request.DefaultData, dataCheck);

            dataCheck.UpdatedDate = DateTime.UtcNow;
            dataCheck.UpdatedUserId = request.UserId;

            #region Assign user

            if (request.DefaultData.UsersAssign != null && request.DefaultData.UsersAssign.Count > 0)
            {
                if (request.DefaultData.UsersAssign.Count == 1)
                {
                    if (dataCheck.AssignUserId != request.DefaultData.UsersAssign[0])
                    {
                        dataCheck.AssignUserId = request.DefaultData.UsersAssign[0];
                    }
                }
                else
                {
                    var taskUsersId = dataCheck.TaskUsers?.Select(s => s.Id).ToList();
                    if (taskUsersId.Any())
                    {
                        var taskUserDeletes = dataCheck.TaskUsers.Where(s => !request.DefaultData.UsersAssign.Contains(s.Id)).ToList();
                        _unitOfWork.Repository<JM_TaskUser>().RemoveRange(taskUserDeletes);
                    }
                    for (int i = 0; i < request.DefaultData.UsersAssign.Count; i++)
                    {
                        if (taskUsersId.Any(s => s == request.DefaultData.UsersAssign[i]))
                            continue;
                        var taskUser = new JM_TaskUser
                        {
                            Id = Guid.NewGuid(),
                            TaskId = dataCheck.Id,
                            UserId = request.DefaultData.UsersAssign[i],
                            IsDelete = false,
                            CompanyId = request.CompanyId,
                            CreatedDate = DateTime.UtcNow,
                            CreatedUserId = request.UserId,
                        };
                        _unitOfWork.Repository<JM_TaskUser>().Add(taskUser);
                    }
                }
            }

            #endregion

            #region Dynamic task data

            var templateDetails = dataCheck.TaskType.Template?.TemplateDetails.ToList();
            var dataDynamics = request.DynamicData;

            if (templateDetails != null && templateDetails.Count > 0)
            {
                var taskCustomColumnValues = dataCheck.TaskCustomColumnValues != null ? dataCheck.TaskCustomColumnValues.ToList() : new List<JM_TaskCustomColumnValue>();
                foreach (var value in dataDynamics)
                {
                    var templateDetail = templateDetails.Where(s => s.Id.Equals(value.Key)).FirstOrDefault();
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
                                CreatedDate = DateTime.UtcNow,
                                CreatedUserId = request.UserId,
                                IsDelete = false,
                                CompanyId = request.CompanyId,
                                Value = value.Value.ToString(),
                            };
                            _unitOfWork.Repository<JM_TaskCustomColumnValue>().Add(taskCustomColumns);
                        }
                    }
                }
            }

            #endregion

            #region Tags
            if (request.DefaultData.Tags != null && request.DefaultData.Tags.Count > 0)
            {
                var tagOld = dataCheck.TaskTags?.ToList();
                foreach (var tag in request.DefaultData.Tags)
                {
                    if (!tag.IsDelete)
                    {
                        if (tag.IsAddNew)
                        {
                            _unitOfWork.Repository<JM_Tag>().Add(new JM_Tag
                            {
                                CompanyId = request.CompanyId,
                                CreatedDate = DateTime.UtcNow,
                                CreatedUserId = request.UserId,
                                Id = tag.Id.Value,
                                Name = tag.Name,
                                IsDelete = false,
                            });
                        }
                        else
                        {
                            var checkTag = tagOld.Where(s => s.TagId == tag.Id).FirstOrDefault();
                            if (checkTag != null)
                                continue;
                        }

                        _unitOfWork.Repository<JM_TaskTag>().Add(new JM_TaskTag
                        {
                            IsDelete = false,
                            Id = Guid.NewGuid(),
                            TagId = tag.Id.Value,
                            TaskId = dataCheck.Id,
                            CompanyId = request.CompanyId,
                            CreatedDate = DateTime.UtcNow,
                            CreatedUserId = request.UserId,
                        });
                    }
                    else
                    {
                        var checkTag = tagOld.Where(s => s.TagId == tag.Id).FirstOrDefault();
                        if (checkTag == null)
                            continue;
                        checkTag.IsDelete = true;

                        _unitOfWork.Repository<JM_TaskTag>().Update(checkTag);
                    }
                }
            }
            #endregion

            #region Task child delete
            if (request.DefaultData.TaskChildDelete != null && request.DefaultData.TaskChildDelete.Count > 0)
            {
                var taskChildDeletes = await _unitOfWork.Repository<JM_Task>().Where(s => request.DefaultData.TaskChildDelete.Contains(s.Id)).ToListAsync();
                foreach (var item in taskChildDeletes)
                {
                    item.ParentId = null;
                    item.UpdatedDate = DateTime.UtcNow;
                    item.UpdatedUserId = request.UserId;
                    _unitOfWork.Repository<JM_Task>().Update(item);
                }
            }
            #endregion

            #region Task child add
            if (request.DefaultData.TaskChild != null && request.DefaultData.TaskChild.Count > 0)
            {
                var taskChildDeletes = await _unitOfWork.Repository<JM_Task>().Where(s => request.DefaultData.TaskChild.Contains(s.Id)).ToListAsync();
                foreach (var item in taskChildDeletes)
                {
                    item.ParentId = dataCheck.Id;
                    item.UpdatedDate = DateTime.UtcNow;
                    item.UpdatedUserId = request.UserId;
                    _unitOfWork.Repository<JM_Task>().Update(item);
                }
            }
            #endregion

            #region Files
            if (request.DefaultData.Files != null && request.DefaultData.Files.Count > 0)
            {
                var fileAddNew = request.DefaultData.Files.Where(s => s.IsAddNew).Select(s => new CreateAttachedFilesRequest
                {
                    EntityId = dataCheck.Id,
                    CompanyId = request.CompanyId,
                    Url = s.Url,
                    UserId = request.UserId,
                    File = s.File,
                }).ToList();
                await _attachedFileService.AddAttachedFiles(fileAddNew);

                var fileDelete = request.DefaultData.Files.Where(s => s.IsDelete).Select(s => s.Id).ToList();
                if (fileDelete != null && fileDelete.Count > 0)
                {
                    await _attachedFileService.RemoveAttachedFiles(fileDelete);
                }
            }
            #endregion

            //#region Comments
            //var comments = request.Comments != null ? request.Comments.Where(s => s.IsAddNew).ToList() : null;
            //if (comments != null && comments.Count > 0)
            //{
            //    foreach (var item in comments)
            //    {
            //        var comment = new JM_Comment
            //        {
            //            Value = item.Value,
            //            Id = Guid.NewGuid(),
            //            CompanyId = request.CompanyId,
            //            CreatedUserId = request.UserId,
            //            UpdatedUserId = request.UserId,
            //        };
            //        _unitOfWork.Repository<JM_Comment>().Add(comment);
            //        _unitOfWork.Repository<JM_CommentTask>().Add(new JM_CommentTask
            //        {
            //            TaskId = dataCheck.Id,
            //            CommentId = comment.Id,
            //            CreatedUserId = request.UserId,
            //            CompanyId = request.CompanyId,
            //            UpdatedUserId = request.UserId,
            //        });
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
