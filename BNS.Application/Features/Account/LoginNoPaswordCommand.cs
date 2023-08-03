using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Domain;
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
using Newtonsoft.Json;
using BNS.Domain.Interface;
using BNS.Data;

namespace BNS.Service.Features
{
    public class LoginNoPaswordCommand : IRequestHandler<LoginNoPasswordRequest, ApiResult<LoginResponse>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IUnitOfWork _unitOfWork;
        protected readonly MyConfiguration _config;
        private readonly IAccountService _accountService;

        public LoginNoPaswordCommand(
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
        public async Task<ApiResult<LoginResponse>> Handle(LoginNoPasswordRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<LoginResponse>();

            var user = await _unitOfWork.JM_AccountRepository.FirstOrDefaultAsync(s => s.UserName == request.Username);
            if (user == null)
            {
                response.errorCode = EErrorCode.Failed.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_UserOrPasswordNotCorrect];
                return response;
            }
            var userCompanys = await _unitOfWork.JM_AccountCompanyRepository.GetAsync(s => s.UserId == user.Id);
            if (!userCompanys.Any(s => s.Status == EUserStatus.ACTIVE))
            {
                response.errorCode = EErrorCode.Failed.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_UserOrPasswordNotCorrect];
                return response;
            }

            var userCompany = await userCompanys.Where(s => s.IsDefault && s.Status == EUserStatus.ACTIVE).Include(s=>s.JM_Company).FirstOrDefaultAsync();

            var roles = new List<string>();
            //if (userCompany.IsMainAccount)
            //    roles.Add(EAccountType.SupperAdmin.ToString());
            //else
            //    roles.Add(EAccountType.User.ToString());

            var claims = new[]
            {
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserId", user.Id.ToString()),
                new Claim("DefaultOrganization",userCompany?.JM_Company.Organization),
                new Claim("CompanyId",userCompany?.CompanyId.ToString()),
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

            response.data = await _accountService.GetUserLoginInfo(user);
            return response;
        }
    }
}
