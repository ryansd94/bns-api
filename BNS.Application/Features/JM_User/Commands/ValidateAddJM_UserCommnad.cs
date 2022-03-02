using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Domain.Responses;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Nest;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;

namespace BNS.Service.Features
{
    public class ValidateAddJM_UserCommnad : IRequestHandler<ValidateAddJM_UserRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly IElasticClient _elasticClient;
        protected readonly MyConfiguration _config;
        private readonly ICipherService _cipherService;
        private readonly IUnitOfWork _unitOfWork;
        public ValidateAddJM_UserCommnad(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IOptions<MyConfiguration> config,
        ICipherService CipherService,
         IElasticClient elasticClient,
         IUnitOfWork unitOfWork)
        {
            _sharedLocalizer = sharedLocalizer;
            _elasticClient = elasticClient;
            _config = config.Value;
            _cipherService = CipherService;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<Guid>> Handle(ValidateAddJM_UserRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var data = JsonConvert.DeserializeObject<JoinTeamResponse>(_cipherService.DecryptString(request.Token));
            if (data.Key != _config.Default.CipherKey)
            {
                response.errorCode = EErrorCode.TokenNotValid.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_TokenNotValid];
                return response;
            }
            var user = await _unitOfWork.JM_AccountCompanyRepository.GetAsync(s => s.CompanyId == data.CompanyId);
            if (user != null)
            {
                response.errorCode = EErrorCode.UserHasJoinTeam.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_ExistsUser];
                return response;
            }
            return response;
        }

    }
}
