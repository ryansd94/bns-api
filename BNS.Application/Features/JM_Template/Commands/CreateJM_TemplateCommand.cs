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
            var dataCheck = await _unitOfWork.JM_TemplateRepository.FirstOrDefaultAsync(s => s.Name.Equals(request.Name) &&
            s.CompanyId ==request.CompanyId);
            if (dataCheck != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }
            var data = new JM_Template
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CreatedDate = DateTime.UtcNow,
                CreatedUser = request.UserId,
                IssueType = JsonConvert.SerializeObject(request.IssueTypes),
                AssigneeIssueStatus =JsonConvert.SerializeObject(request.AssigneeIssueStatus),
                ReporterIssueStatus = JsonConvert.SerializeObject(request.ReporterIssueStatus)

            };

            await _unitOfWork.JM_TemplateRepository.AddAsync(data);
            await _unitOfWork.SaveChangesAsync();
            response.data = data.Id;
            return response;
        }

    }
}
