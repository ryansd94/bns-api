using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;

        public CreateTemplateCommand(
         IMapper mapper,
         IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResult<Guid>> Handle(CreateTemplateRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<JM_Template>().FirstOrDefaultAsync(s => s.Name.Equals(request.Name) &&
            s.CompanyId == request.CompanyId && !s.IsDelete);
            if (dataCheck != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = LocalizedBackendMessages.MSG_ExistsData;
                return response;
            }
            var data = _mapper.Map<JM_Template>(request);

            if (request.Status != null && request.Status.Count > 0)
            {
                var templateStatusOrder = 0;
                foreach (var item in request.Status)
                {
                    var templateStatus = new JM_TemplateStatus
                    {
                        Id = Guid.NewGuid(),
                        CompanyId = request.CompanyId,
                        TemplateId = data.Id,
                        StatusId = item.Id,
                        Order = templateStatusOrder,
                        CreatedDate = DateTime.UtcNow,
                        CreatedUserId = request.UserId,
                    };
                    await _unitOfWork.Repository<JM_TemplateStatus>().AddAsync(templateStatus);
                    templateStatusOrder += 1;
                }
            }

            if (request.Content != null)
            {
                var contenObjects = request.Content;
                var columnObjects = new List<ColumnObject>();

                columnObjects.Add(new ColumnObject
                {
                    ColumnPosition = EColumnPosition.Column1,
                    Column = contenObjects.column1
                });
                columnObjects.Add(new ColumnObject
                {
                    ColumnPosition = EColumnPosition.Column2,
                    Column = contenObjects.column2
                });
                columnObjects.Add(new ColumnObject
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
                                TemplateId = data.Id,
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
                                        TemplateId = data.Id,
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

                var content = new ColumnItemRoot
                {
                    column1 = columnObjects.Where(s => s.ColumnPosition == EColumnPosition.Column1).FirstOrDefault()?.Column,
                    column2 = columnObjects.Where(s => s.ColumnPosition == EColumnPosition.Column2).FirstOrDefault()?.Column,
                    column3 = columnObjects.Where(s => s.ColumnPosition == EColumnPosition.Column3).FirstOrDefault()?.Column,
                };
                data.Content = Newtonsoft.Json.JsonConvert.SerializeObject(content);
            }
            //return response;
            await _unitOfWork.Repository<JM_Template>().AddAsync(data);
            await _unitOfWork.SaveChangesAsync();
            response.data = data.Id;
            return response;
        }

    }
}
