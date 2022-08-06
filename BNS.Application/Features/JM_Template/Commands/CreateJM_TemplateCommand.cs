using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Domain;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using Newtonsoft.Json;
using BNS.Data.Entities.JM_Entities;
using Microsoft.EntityFrameworkCore;

namespace BNS.Service.Features
{
    public class CreateJM_TemplateCommand : IRequestHandler<CreateJM_TemplateRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public CreateJM_TemplateCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(CreateJM_TemplateRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<JM_Template>().FirstOrDefaultAsync(s => s.Name.Equals(request.Name) &&
            s.CompanyId == request.CompanyId);
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
                CreatedUser = request.UserId,
                Content = request.Content,
                CompanyId = request.CompanyId,
            };

            if (request.Status != null && request.Status.Count > 0)
            {
                foreach (var item in request.Status)
                {
                    if (item.IsNew == true)
                    {
                        var status = new JM_Status
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Color = item.Color,
                        };
                        await _unitOfWork.Repository<JM_Status>().AddAsync(status);
                    }
                    var templateStatus = new JM_TemplateStatus
                    {
                        Id = Guid.NewGuid(),
                        CompanyId = request.CompanyId,
                        TemplateId = template.Id,
                        StatusId = item.Id
                    };
                    await _unitOfWork.Repository<JM_TemplateStatus>().AddAsync(templateStatus);
                }
            }

            await _unitOfWork.Repository<JM_Template>().AddAsync(template);
            await _unitOfWork.SaveChangesAsync();
            response.data = template.Id;
            return response;
        }

    }
}
