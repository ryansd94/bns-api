using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Domain.Queries;
using BNS.Domain.Responses;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Service.Features
{
    public class CheckOrganizationCommand : IRequestHandler<CheckOrganizationRequest, ApiResult<CheckOrganizationResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public CheckOrganizationCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<ApiResult<CheckOrganizationResponse>> Handle(CheckOrganizationRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<CheckOrganizationResponse>();
            response.data.IsValid = true;
            var dataCheck = await _unitOfWork.Repository<JM_Company>().FirstOrDefaultAsync(s => s.Organization == request.Domain && !s.IsDelete);
            if (dataCheck != null)
            {
                response.data.IsValid = false;
                return response;
            }
            return response;
        }

    }
}
