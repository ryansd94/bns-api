using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Domain.Responses;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;

namespace BNS.Service.Features
{
    public class ValidateSignupCommand : IRequestHandler<ValidateSignupRequest, ApiResult<ValidateUserResponse>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly MyConfiguration _config;
        private readonly ICipherService _cipherService;

        public ValidateSignupCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IOptions<MyConfiguration> config,
        ICipherService CipherService)
        {
            _sharedLocalizer = sharedLocalizer;
            _config = config.Value;
            _cipherService = CipherService;
        }

        public async Task<ApiResult<ValidateUserResponse>> Handle(ValidateSignupRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<ValidateUserResponse>();
            var tokenValue = await _cipherService.DecryptString(request.Token);
            var data = JsonConvert.DeserializeObject<JoinTeamResponse>(tokenValue);
            if (data == null)
            {
                response.errorCode = EErrorCode.TokenNotValid.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_TokenNotValid];
                return response;
            }
            response.data.Status = EUserValidate.OK;
            return response;
        }

    }
}
