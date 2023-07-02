using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using BNS.Domain;
using BNS.Domain.Responses;
using BNS.Infrastructure;
using BNS.Domain.Interface;
using Newtonsoft.Json;

namespace BNS.Service.Features
{
    public class LoginGoogleCommand : IRequestHandler<LoginGoogleRequest, ApiResult<LoginResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly MyConfiguration _config;
        private static FirebaseAdmin.FirebaseApp _firebaseApp = null;
        private readonly ICipherService _cipherService;
        private readonly IAccountService _accountService;

        public LoginGoogleCommand(
         IUnitOfWork unitOfWork,
         IOptions<MyConfiguration> config,
         ICipherService CipherService,
         IStringLocalizer<SharedResource> sharedLocalizer,
         IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _config = config.Value;
            _sharedLocalizer = sharedLocalizer;
            _cipherService = CipherService;
            _accountService = accountService;
        }
        private FirebaseAdmin.FirebaseApp DefaultFirebaseApp
        {
            get
            {
                if (_firebaseApp == null)
                {
                    if (FirebaseAdmin.FirebaseApp.DefaultInstance == null)
                    {
                        _firebaseApp = FirebaseAdmin.FirebaseApp.Create(new FirebaseAdmin.AppOptions()
                        {
                            Credential = Google.Apis.Auth.OAuth2.GoogleCredential.FromFile(_config.Firebase.FireBaseAccount).CreateScoped("https://www.googleapis.com/auth/firebase.messaging")
                        });
                    }
                    else
                    {
                        _firebaseApp = FirebaseAdmin.FirebaseApp.DefaultInstance;
                    }
                }
                return _firebaseApp;
            }
        }
        public async Task<ApiResult<LoginResponse>> Handle(LoginGoogleRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<LoginResponse>();

            var infoUser = await Firebase.CheckTokenGoogle(request.Token, DefaultFirebaseApp);
            if (infoUser == null)
            {
                response.errorCode = EErrorCode.Failed.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }
            var email = infoUser.email;
            var id = infoUser.sub;
            var user = await _unitOfWork.JM_AccountRepository.FirstOrDefaultAsync(s => s.Email.Equals(email), x => x.AccountCompanys);
            if (user == null)
            {
                response.data.Token = _cipherService.EncryptString(JsonConvert.SerializeObject(new JoinTeamResponse
                {
                    EmailJoin = infoUser.email.ToString()
                }));
                response.errorCode = EErrorCode.UserNotRegister.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_UserNotRegister];
                return response;
            }

            response.data = await _accountService.GetUserLoginInfo(user);
            return response;
        }
    }
}
