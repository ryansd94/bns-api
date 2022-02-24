using BNS.Data.Entities;
using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.ViewModels;
using BNS.ViewModels.Responses;
using FirebaseAdmin.Auth;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
namespace BNS.Application.Features
{
    public class LoginGoogleCommand
    {
        public class LoginGoogleRequest : CommandBase<ApiResult<CF_AccountLoginResponseModel>>
        {
            [Required]
            public string Token { get; set; }
        }
        public class LoginGoogleCommandHandler : IRequestHandler<LoginGoogleRequest, ApiResult<CF_AccountLoginResponseModel>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
            protected readonly MyConfiguration _config;
            private static FirebaseAdmin.FirebaseApp _firebaseApp = null;

            public LoginGoogleCommandHandler(BNSDbContext context,
             IOptions<MyConfiguration> config,
             IStringLocalizer<SharedResource> sharedLocalizer)
            {
                _context = context;
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
            public async Task<ApiResult<CF_AccountLoginResponseModel>> Handle(LoginGoogleRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<CF_AccountLoginResponseModel>();

                var infoUser = await CheckTokenGoogle(request.Token);
                if (infoUser == null)
                {
                    response.errorCode = EErrorCode.Failed.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                    return response;
                }
                var email = infoUser.email;
                var id = infoUser.sub;
                response.data = new CF_AccountLoginResponseModel();
                var user = await _context.JM_Accounts.Where(s => s.Email.Equals(email)).Include(s => s.JM_AccountCompanys).FirstOrDefaultAsync();
                var companyId = Guid.Empty;
                if (user == null)
                {
                    var userid = Guid.NewGuid();
                    var company = new JM_Company
                    {
                        Id = Guid.NewGuid(),
                        IsDelete = false,
                        CreatedDate = DateTime.UtcNow,
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
                        IsMainAccount = true,
                    };

                    var accountCompany = new JM_AccountCompany
                    {
                        Id = Guid.NewGuid(),
                        IsDelete = false,
                        UserId = userid,
                        CompanyId = company.Id,
                        Status=EStatus.ACTIVE,
                        CreatedDate = DateTime.UtcNow,
                        CreatedUser = userid,
                        FullName = infoUser.name.ToString(),
                        Email = infoUser.email.ToString(),
                    };
                    company.CreatedUser = user.Id;
                    companyId = company.Id;
                    await _context.JM_Companys.AddAsync(company);
                    await _context.JM_Accounts.AddAsync(user);
                    await _context.JM_AccountCompanys.AddAsync(accountCompany);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    companyId = user.JM_AccountCompanys.FirstOrDefault().CompanyId;
                }

                var roles = new List<string>();
                if (user.IsMainAccount != null && user.IsMainAccount.Value)
                    roles.Add(EAccountType.SupperAdmin.ToString());
                else
                    roles.Add(EAccountType.User.ToString());
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
            private async Task<GoogleApiTokenInfo> CheckTokenGoogle(string token)
            {
                var result = new GoogleApiTokenInfo();
                FirebaseToken decodedToken = await FirebaseAuth.GetAuth(DefaultFirebaseApp)
    .VerifyIdTokenAsync(token);
                if (decodedToken == null)
                    return null;
                result.sub = decodedToken.Uid;
                result.email = decodedToken.Claims.Where(s => s.Key == "email").FirstOrDefault().Value;
                result.name = decodedToken.Claims.Where(s => s.Key == "name").FirstOrDefault().Value;
                return result;


            }
            private class GoogleApiTokenInfo
            {

                public string sub { get; set; }

                /// <summary>
                /// The user's email address. This may not be unique and is not suitable for use as a primary key. Provided only if your scope included the string "email".
                /// </summary>
                public object email { get; set; }

                public object name { get; set; }

                /// <summary>
                /// The URL of the user's profile picture. Might be provided when:
                /// The request scope included the string "profile"
                /// The ID token is returned from a token refresh
                /// When picture claims are present, you can use them to update your app's user records. Note that this claim is never guaranteed to be present.
                /// </summary>
                public string picture { get; set; }

                public string given_name { get; set; }

                public string family_name { get; set; }

                public string locale { get; set; }

                public string alg { get; set; }

                public string kid { get; set; }
            }

        }
    }
}
