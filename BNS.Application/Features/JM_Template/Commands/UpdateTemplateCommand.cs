using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Service.Features
{
    public class UpdateTemplateCommand : IRequestHandler<UpdateTemplateRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public UpdateTemplateCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(UpdateTemplateRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<JM_Template>()
                .Include(s => s.TemplateStatus)
                .Include(s => s.TemplateDetails)
                .FirstOrDefaultAsync(s => s.Id == request.Id &&
                s.CompanyId == request.CompanyId);
            if (dataCheck == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }
            var statusIds = dataCheck.TemplateStatus.Select(s => s.StatusId);
            dataCheck.Name = request.Name;
            dataCheck.Description = request.Description;
            dataCheck.UpdatedDate = DateTime.UtcNow;
            dataCheck.UpdatedUserId = request.UserId;

            if (request.Status != null && request.Status.Count > 0)
            {
                var statusAddNew = request.Status.Where(s => !statusIds.Contains(s.Id)).ToList();
                var statusDeleteIds = request.Status.Where(s => s.IsDelete).Select(s => s.Id).ToList();
                foreach (var item in statusAddNew)
                {
                    var statusId = item.Id;
                    var templateStatus = new JM_TemplateStatus
                    {
                        Id = Guid.NewGuid(),
                        CompanyId = request.CompanyId,
                        TemplateId = dataCheck.Id,
                        StatusId = statusId
                    };
                    await _unitOfWork.Repository<JM_TemplateStatus>().AddAsync(templateStatus);
                }
                if (statusDeleteIds.Count > 0)
                {
                    var statusTemplateDelete = await _unitOfWork.Repository<JM_TemplateStatus>().Where(s => s.TemplateId == request.Id &&
                    statusDeleteIds.Contains(s.StatusId)).ToListAsync();
                    _unitOfWork.Repository<JM_TemplateStatus>().RemoveRange(statusTemplateDelete);
                }
            }


            if (request.Content != null)
            {
                var contenObjects = request.Content;
                var columnObjects = new List<JM_ColumnObject>();
                var templateDetails = dataCheck.TemplateDetails;
                if (templateDetails != null)
                {
                    foreach (var item in templateDetails)
                    {
                        item.IsDelete = true;
                        _unitOfWork.Repository<JM_TemplateDetail>().Update(item);
                    }
                }
                columnObjects.Add(new JM_ColumnObject
                {
                    ColumnPosition = EColumnPosition.Column1,
                    Column = contenObjects.column1
                });
                columnObjects.Add(new JM_ColumnObject
                {
                    ColumnPosition = EColumnPosition.Column2,
                    Column = contenObjects.column2
                });
                columnObjects.Add(new JM_ColumnObject
                {
                    ColumnPosition = EColumnPosition.Column3,
                    Column = contenObjects.column3
                });
                foreach (var columnObject in columnObjects)
                {
                    if (columnObject.Column != null)
                    {
                        for (int i = 0; i <= columnObject.Column.Count - 1; i++)
                        {
                            var item = columnObject.Column[i];
                            var templateDetailId = Guid.NewGuid();
                            Guid customColumnId = Guid.Empty;
                            Guid.TryParse(item.columnId, out customColumnId);
                            Enum.TryParse(item.type, out EControlType controlType);
                            if (!item.@default)
                            {
                                if (item.isAddNew)
                                {
                                    if (customColumnId == Guid.Empty)
                                        customColumnId = Guid.NewGuid();

                                    var column = new JM_CustomColumn
                                    {
                                        Id = customColumnId,
                                        CompanyId = request.CompanyId,
                                        ControlType = controlType,
                                        CreatedDate = DateTime.UtcNow,
                                        CreatedUserId = request.UserId,
                                        IsDelete = false,
                                        Name = item.label
                                    };
                                    await _unitOfWork.Repository<JM_CustomColumn>().AddAsync(column);
                                }
                            }
                            JM_TemplateDetail templateDetailExists = null;
                            if (customColumnId != Guid.Empty)
                            {
                                templateDetailExists = templateDetails.Where(s => s.CustomColumnId == customColumnId).FirstOrDefault();
                            }
                            else
                            {
                                templateDetailExists = templateDetails.Where(s => s.ColumnName == item.name).FirstOrDefault();
                            }
                            if (templateDetailExists == null)
                            {
                                var templateDetail = new JM_TemplateDetail
                                {
                                    Id = templateDetailId,
                                    CustomColumnId = !item.@default ? customColumnId : null,
                                    ColumnPosition = columnObject.ColumnPosition,
                                    CompanyId = request.CompanyId,
                                    CreatedDate = DateTime.UtcNow,
                                    CreatedUserId = request.UserId,
                                    IsDelete = false,
                                    TemplateId = dataCheck.Id,
                                    ColumnName = item.name,
                                    ColumnTitle = item.label,
                                    Order = i
                                };
                                await _unitOfWork.Repository<JM_TemplateDetail>().AddAsync(templateDetail);
                            }
                            else
                            {
                                templateDetailId = templateDetailExists.Id;
                                templateDetailExists.ColumnPosition = columnObject.ColumnPosition;
                                templateDetailExists.ColumnName = item.name;
                                templateDetailExists.ColumnTitle = item.label;
                                templateDetailExists.Order = i;
                                templateDetailExists.IsDelete = true;
                                _unitOfWork.Repository<JM_TemplateDetail>().Update(templateDetailExists);
                            }
                            item.id = templateDetailId.ToString();
                            if (controlType == EControlType.Group)
                            {
                                item.prefix = String.Format("{0}@{1}", item.id, columnObject.ColumnPosition.ToString().ToLower());
                            }

                            if (item.items != null)
                            {
                                var orderChild = 0;
                                foreach (var child in item.items)
                                {
                                    Guid columnChildId = Guid.Empty;
                                    Guid.TryParse(child.columnId, out columnChildId);
                                    Enum.TryParse(child.type, out EControlType childType);
                                    if (child.isAddNew)
                                    {
                                        if (columnChildId == Guid.Empty)
                                            columnChildId = Guid.NewGuid();
                                        var column = new JM_CustomColumn
                                        {
                                            Id = columnChildId,
                                            CompanyId = request.CompanyId,
                                            ControlType = childType,
                                            CreatedDate = DateTime.UtcNow,
                                            CreatedUserId = request.UserId,
                                            IsDelete = false,
                                            Name = child.label
                                        };
                                        await _unitOfWork.Repository<JM_CustomColumn>().AddAsync(column);
                                        //await _unitOfWork.SaveChangesAsync();
                                    }

                                    JM_TemplateDetail templateChildDetailExists = null;
                                    if (columnChildId != Guid.Empty)
                                    {
                                        templateChildDetailExists = templateDetails.Where(s => s.CustomColumnId == columnChildId).FirstOrDefault();
                                    }
                                    else
                                    {
                                        templateChildDetailExists = templateDetails.Where(s => s.ColumnName == child.name).FirstOrDefault();
                                    }
                                    if (templateChildDetailExists == null)
                                    {
                                        templateChildDetailExists = new JM_TemplateDetail
                                        {
                                            Id = Guid.NewGuid(),
                                            CustomColumnId = !child.@default ? columnChildId : null,
                                            ColumnPosition = columnObject.ColumnPosition,
                                            CompanyId = request.CompanyId,
                                            CreatedDate = DateTime.UtcNow,
                                            CreatedUserId = request.UserId,
                                            IsDelete = false,
                                            ParentId = templateDetailId,
                                            ColumnName = child.name,
                                            ColumnTitle = child.label,
                                            TemplateId = dataCheck.Id,
                                            Order = orderChild
                                        };
                                        await _unitOfWork.Repository<JM_TemplateDetail>().AddAsync(templateChildDetailExists);
                                    }
                                    else
                                    {
                                        templateChildDetailExists.ColumnPosition = columnObject.ColumnPosition;
                                        templateChildDetailExists.ColumnName = item.name;
                                        templateChildDetailExists.ColumnTitle = item.label;
                                        templateChildDetailExists.Order = i;
                                        templateChildDetailExists.IsDelete = true;
                                        _unitOfWork.Repository<JM_TemplateDetail>().Update(templateChildDetailExists);
                                    }

                                    child.id = String.Format("{0}@{1}", templateChildDetailExists.Id, templateDetailExists.Id);
                                    orderChild += 1;
                                }
                            }
                        }
                    }
                }

                var xxxx = new JM_ColumnItemRoot
                {
                    column1 = columnObjects.Where(s => s.ColumnPosition == EColumnPosition.Column1).FirstOrDefault()?.Column,
                    column2 = columnObjects.Where(s => s.ColumnPosition == EColumnPosition.Column2).FirstOrDefault()?.Column,
                    column3 = columnObjects.Where(s => s.ColumnPosition == EColumnPosition.Column3).FirstOrDefault()?.Column,
                };
                dataCheck.Content = Newtonsoft.Json.JsonConvert.SerializeObject(xxxx);
            }

            _unitOfWork.Repository<JM_Template>().Update(dataCheck);
            await _unitOfWork.SaveChangesAsync();
            response.data = dataCheck.Id;
            return response;
        }

    }
}
