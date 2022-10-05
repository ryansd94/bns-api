using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
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
using BNS.Domain;
using BNS.Domain.Responses;
using BNS.Infrastructure;

namespace BNS.Service.Features
{
    public class LoginGoogleCommand : IRequestHandler<LoginGoogleRequest, ApiResult<LoginResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly MyConfiguration _config;
        private static FirebaseAdmin.FirebaseApp _firebaseApp = null;

        public LoginGoogleCommand(
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
            var companyId = Guid.Empty;
            if (user == null)
            {
                response.errorCode = EErrorCode.UserNotRegister.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_UserNotRegister];
                return response;
            }
            else
            {
                companyId = user.AccountCompanys.FirstOrDefault().CompanyId;
            }

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
                , expires: DateTime.UtcNow.AddDays(71)
                , signingCredentials: creds
                );
            response.data.FullName = user.FullName;
            response.data.Image = user.Image;
            response.data.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return response;
        }

    }
}
