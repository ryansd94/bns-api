using BNS.Data.Entities.JM_Entities;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using BNS.Domain.Responses;
using BNS.Domain;
using BNS.Infrastructure;
using BNS.Utilities;
using BNS.Domain.Interface;
using BNS.Data;

namespace BNS.Service.Features
{
    public class RegisterWithGoogleCommand : IRequestHandler<RegisterGoogleRequest, ApiResult<LoginResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly MyConfiguration _config;
        private static FirebaseAdmin.FirebaseApp _firebaseApp = null;
        private readonly IAccountService _accountService;

        public RegisterWithGoogleCommand(
         IUnitOfWork unitOfWork,
         IOptions<MyConfiguration> config,
         IStringLocalizer<SharedResource> sharedLocalizer,
         IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _config = config.Value;
            _sharedLocalizer = sharedLocalizer;
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
        public async Task<ApiResult<LoginResponse>> Handle(RegisterGoogleRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<LoginResponse>();

            var infoUser = await Firebase.CheckTokenGoogle(request.GoogleToken, DefaultFirebaseApp);
            if (infoUser == null)
            {
                response.errorCode = EErrorCode.Failed.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }
            var email = infoUser.email;
            var id = infoUser.sub;
            response.data = new LoginResponse();
            var user = await _unitOfWork.JM_AccountRepository.FirstOrDefaultAsync(s => s.Email.Equals(email));
            if (user != null)
            {
                response.errorCode = EErrorCode.Failed.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_UserHasRegister];
                return response;
            }
            var companyId = Guid.Empty;

            var userid = Guid.NewGuid();
            var company = new JM_Company
            {
                Id = Guid.NewGuid(),
                IsDelete = false,
                CreatedDate = DateTime.UtcNow,
                Name = email.ToString().Split("@")[0],
                Organization = request.Organization,
                UserType = request.UserType,
                Scale = request.Scale
            };

            user = new JM_Account
            {
                Id = userid,
                UserName = infoUser.email.ToString(),
                Email = infoUser.email.ToString(),
                CreatedDate = DateTime.UtcNow,
                CreatedUser = userid,
                IsDelete = false,
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                GoogleId = id,
                IsActive = true,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            user.PasswordHash = Ultility.MD5Encrypt(request.Password);
            var accountCompany = new JM_AccountCompany
            {
                Id = Guid.NewGuid(),
                IsDelete = false,
                UserId = userid,
                CompanyId = company.Id,
                Status = EUserStatus.ACTIVE,
                CreatedDate = DateTime.UtcNow,
                CreatedUser = userid,
                Email = infoUser.email.ToString(),
                IsDefault = true,
                IsMainAccount = true,
            };
            company.CreatedUser = user.Id;
            companyId = company.Id;
            await _unitOfWork.JM_CompanyRepository.AddAsync(company);
            await _unitOfWork.JM_AccountRepository.AddAsync(user);
            await _unitOfWork.JM_AccountCompanyRepository.AddAsync(accountCompany);
            await _unitOfWork.SaveChangesAsync();

            response = await _accountService.GetUserLoginInfo(user);
            return response;
        }
    }
}
