using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using FirebaseAdmin.Auth;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using BNS.Domain.Responses;
using BNS.Domain;
using BNS.Infrastructure;

namespace BNS.Service.Features
{
    public class RegisterWithGoogleCommand : IRequestHandler<RegisterGoogleRequest, ApiResult<LoginResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly MyConfiguration _config;
        private static FirebaseAdmin.FirebaseApp _firebaseApp = null;

        public RegisterWithGoogleCommand(
         IUnitOfWork unitOfWork,
         IOptions<MyConfiguration> config,
         IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _config = config.Value;
            _sharedLocalizer = sharedLocalizer;
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

            var infoUser = await Firebase.CheckTokenGoogle(request.Token, DefaultFirebaseApp);
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
                Name=email.ToString().Split("@")[0]
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
                FullName = infoUser.name.ToString(),
            };

            var accountCompany = new JM_AccountCompany
            {
                Id = Guid.NewGuid(),
                IsDelete = false,
                UserId = userid,
                CompanyId = company.Id,
                Status=EUserStatus.ACTIVE,
                CreatedDate = DateTime.UtcNow,
                CreatedUser = userid,
                Email = infoUser.email.ToString(),
                IsDefault=true,
                IsMainAccount = true,
            };
            company.CreatedUser = user.Id;
            companyId = company.Id;
            await _unitOfWork.JM_CompanyRepository.AddAsync(company);
            await _unitOfWork.JM_AccountRepository.AddAsync(user);
            await _unitOfWork.JM_AccountCompanyRepository.AddAsync(accountCompany);
            await _unitOfWork.SaveChangesAsync();


            var roles = new List<string>();
            var claims = new[]
            {
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserId", user.Id.ToString()),
                new Claim("CompanyId", companyId.ToString()),
                new Claim("Role",string.Join(";",roles))
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Tokens.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config.Tokens.Issuer
                , _config.Tokens.Issuer
                , claims
                , expires: DateTime.UtcNow.AddDays(1)
                , signingCredentials: creds
                );
            response.data.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return response;
        }

    }
}
