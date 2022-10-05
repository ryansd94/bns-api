using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
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
            var dataCheck = await _unitOfWork.Repository<JM_Template>().Include(s => s.TemplateStatus).FirstOrDefaultAsync(s => s.Id == request.Id &&
              s.CompanyId == request.CompanyId);
            if (dataCheck == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }
            var statusIds = dataCheck.TemplateStatus.Select(s => s.StatusId);
            dataCheck.Name = request.Name;
            dataCheck.Content = request.Content;
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

            if (!string.IsNullOrEmpty(request.Content))
            {
                var contenObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<JM_ColumnItemRoot>(request.Content);
                var columnObjects = contenObjects.column1;
                columnObjects.AddRange(contenObjects.column2);
                columnObjects.AddRange(contenObjects.column3);
                var columnAddNew = columnObjects.Where(s => s.isAddNew).ToList();
                columnAddNew.AddRange(columnObjects.Where(s => s.items != null).SelectMany(s => s.items.Where(s => s.isAddNew)));
                foreach (var column in columnAddNew)
                {
                    Enum.TryParse(column.type, out EControlType type);
                    await _unitOfWork.Repository<JM_CustomColumn>().AddAsync(new JM_CustomColumn
                    {
                        Id = Guid.NewGuid(),
                        CompanyId = request.CompanyId,
                        ControlType = type,
                        CreatedDate = DateTime.UtcNow,
                        CreatedUserId = request.UserId,
                        IsDelete = false,
                        Name = column.label
                    });

                }
            }

            _unitOfWork.Repository<JM_Template>().Update(dataCheck);
            await _unitOfWork.SaveChangesAsync();
            response.data = dataCheck.Id;
            return response;
        }

    }
}
