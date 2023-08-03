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
using BNS.Data;

namespace BNS.Service.Features
{
    public class ValidateAddUserCommnad : IRequestHandler<ValidateAddUserRequest, ApiResult<ValidateUserResponse>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly MyConfiguration _config;
        private readonly ICipherService _cipherService;
        private readonly IUnitOfWork _unitOfWork;
        public ValidateAddUserCommnad(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IOptions<MyConfiguration> config,
        ICipherService CipherService,
         IUnitOfWork unitOfWork)
        {
            _sharedLocalizer = sharedLocalizer;
            _config = config.Value;
            _cipherService = CipherService;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<ValidateUserResponse>> Handle(ValidateAddUserRequest request, CancellationToken cancellationToken)
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
            var accountCompany = await _unitOfWork.JM_AccountCompanyRepository.FirstOrDefaultAsync(s => s.CompanyId == data.CompanyId && s.Id == data.Id && !s.IsDelete);

            if (accountCompany == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_UserHasDeleted];
                return response;
            }
            if (accountCompany.Status == EUserStatus.ACTIVE)
            {
                response.errorCode = EErrorCode.UserHasJoinTeam.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_UserHasJoinTeam];
                return response;
            }
            var account = await _unitOfWork.JM_AccountRepository.FirstOrDefaultAsync(s => s.Email == data.EmailJoin && !s.IsDelete);
            if (account != null && account.IsActive)
            {
                response.data.Status = EUserValidate.IS_HAS_ACCOUNT;
                return response;
            }
            response.data.Status = EUserValidate.OK;
            return response;
        }

    }
}
