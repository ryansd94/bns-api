using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Domain;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using BNS.Utilities;
using BNS.Domain.Responses;
using BNS.Domain.Interface;
using BNS.Data;

namespace BNS.Service.Features
{
    public class LoginCommand : IRequestHandler<LoginRequest, ApiResult<LoginResponse>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IUnitOfWork _unitOfWork;
        protected readonly MyConfiguration _config;
        private readonly IAccountService _accountService;

        public LoginCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IOptions<MyConfiguration> config,
         IUnitOfWork unitOfWork,
         IAccountService accountService)
        {
            _sharedLocalizer = sharedLocalizer;
            _unitOfWork = unitOfWork;
            _config = config.Value;
            _accountService = accountService;
        }

        public async Task<ApiResult<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<LoginResponse>();

            var user = await _unitOfWork.JM_AccountRepository.FirstOrDefaultAsync(s => s.UserName == request.Username);
            if (user == null)
            {
                response.errorCode = EErrorCode.UserPasswordNotCorrect.ToString();
                return response;
            }

            if (!user.PasswordHash.Equals(Ultility.MD5Encrypt(request.Password)))
            {
                response.errorCode = EErrorCode.UserPasswordNotCorrect.ToString();
                return response;
            }
            response = await _accountService.GetUserLoginInfo(user);
            return response;
        }
    }
}
