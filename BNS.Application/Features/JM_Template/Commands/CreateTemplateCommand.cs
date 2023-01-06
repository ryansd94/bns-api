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
    public class CreateTemplateCommand : IRequestHandler<CreateTemplateRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public CreateTemplateCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(CreateTemplateRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<JM_Template>().FirstOrDefaultAsync(s => s.Name.Equals(request.Name) &&
            s.CompanyId == request.CompanyId && !s.IsDelete);
            if (dataCheck != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }
            var template = new JM_Template
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CreatedDate = DateTime.UtcNow,
                CreatedUserId = request.UserId,
                Content = request.Content,
                CompanyId = request.CompanyId,
            };


            if (request.Status != null && request.Status.Count > 0)
            {
                var templateStatusOrder = 0;
                foreach (var item in request.Status)
                {
                    var statusId = item.Id;
                    if (item.IsNew == true)
                    {
                        var status = new JM_Status
                        {
                            Id = Guid.NewGuid(),
                            Name = item.Name,
                            Color = item.Color,
                            CreatedDate = DateTime.UtcNow,
                            CreatedUserId = request.UserId,
                            CompanyId = request.CompanyId,
                        };
                        statusId = status.Id;
                        await _unitOfWork.Repository<JM_Status>().AddAsync(status);
                    }
                    var templateStatus = new JM_TemplateStatus
                    {
                        Id = Guid.NewGuid(),
                        CompanyId = request.CompanyId,
                        TemplateId = template.Id,
                        StatusId = statusId,
                        Order = templateStatusOrder
                    };
                    await _unitOfWork.Repository<JM_TemplateStatus>().AddAsync(templateStatus);
                    templateStatusOrder += 1;
                }
            }

            if (!string.IsNullOrEmpty(request.Content))
            {
                var contenObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<JM_ColumnItemRoot>(request.Content);
                var columnObjects = new List<JM_ColumnObject>();

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
                            Guid columnId = Guid.Empty;
                            Guid.TryParse(item.columnId, out columnId);
                            Enum.TryParse(item.type, out EControlType controlType);
                            if (!item.@default)
                            {
                                if (item.isAddNew)
                                {
                                    if (columnId == Guid.Empty)
                                        columnId = Guid.NewGuid();

                                    var column = new JM_CustomColumn
                                    {
                                        Id = columnId,
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

                            var templateDetail = new JM_TemplateDetail
                            {
                                Id = templateDetailId,
                                CustomColumnId = !item.@default ? columnId : null,
                                ColumnPosition = columnObject.ColumnPosition,
                                CompanyId = request.CompanyId,
                                CreatedDate = DateTime.UtcNow,
                                CreatedUserId = request.UserId,
                                IsDelete = false,
                                TemplateId = template.Id,
                                ColumnName = item.name,
                                ColumnTitle = item.label,
                                Order = i
                            };
                            item.id = templateDetailId.ToString();
                            if (controlType == EControlType.Group)
                            {
                                item.prefix = String.Format("{0}@{1}", item.id, columnObject.ColumnPosition.ToString().ToLower());
                            }
                            await _unitOfWork.Repository<JM_TemplateDetail>().AddAsync(templateDetail);

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

                                    var templateChildDetail = new JM_TemplateDetail
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
                                        TemplateId = template.Id,
                                        Order = orderChild
                                    };
                                    child.id = String.Format("{0}@{1}", templateChildDetail.Id, templateDetail.Id);
                                    child.customColumnId = templateChildDetail.Id.ToString();
                                    await _unitOfWork.Repository<JM_TemplateDetail>().AddAsync(templateChildDetail);
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
                template.Content = Newtonsoft.Json.JsonConvert.SerializeObject(xxxx);
            }
            //return response;
            await _unitOfWork.Repository<JM_Template>().AddAsync(template);
            await _unitOfWork.SaveChangesAsync();
            response.data = template.Id;
            return response;
        }

    }
}
