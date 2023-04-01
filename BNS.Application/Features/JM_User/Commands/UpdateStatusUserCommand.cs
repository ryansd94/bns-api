using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;

namespace BNS.Service.Features
{
    public class UpdateStatusUserCommand : IRequestHandler<UpdateStatusUserRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly IElasticClient _elasticClient;
        protected readonly MyConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateStatusUserCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IOptions<MyConfiguration> config,
         IElasticClient elasticClient,
         IUnitOfWork unitOfWork)
        {
            _sharedLocalizer = sharedLocalizer;
            _elasticClient = elasticClient;
            _config = config.Value;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<Guid>> Handle(UpdateStatusUserRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var userCompany = await _unitOfWork.JM_AccountCompanyRepository.FirstOrDefaultAsync(s => s.CompanyId == request.CompanyId
            && s.Id == request.Id);
            if (userCompany == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_NotExistsUser];
                return response;
            }
            if(userCompany.IsMainAccount)
            {
                response.errorCode = EErrorCode.Failed.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_CannotActionMainUser];
                return response;
            }
            userCompany.Status= request.Status;
            userCompany.UpdatedDate=DateTime.UtcNow;
            userCompany.UpdatedUser=request.UserId;
            await _unitOfWork.JM_AccountCompanyRepository.UpdateAsync(userCompany);
            await _unitOfWork.SaveChangesAsync();
            return response;
        }
    }
}
