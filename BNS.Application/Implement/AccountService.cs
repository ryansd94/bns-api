using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Domain.Interface;
using BNS.Domain.Responses;
using BNS.Utilities.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Service.Implement
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        protected readonly MyConfiguration _config;

        public AccountService(
         IOptions<MyConfiguration> config,
         IUnitOfWork unitOfWork,
         ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _config = config.Value;
            _cacheService = cacheService;
        }

        public async Task<LoginResponse> GetUserLoginInfo(JM_Account user)
        {
            var result = new LoginResponse();
            var userCompanys = await _unitOfWork.Repository<JM_AccountCompany>().Include(s => s.JM_Company).Where(s => s.UserId == user.Id && s.Status == EUserStatus.ACTIVE).ToListAsync();
            var userCompany = userCompanys.Where(s => s.IsDefault).FirstOrDefault();
            if (userCompany != null)
            {
                var viewPermissions = await GetViewPermissionByUser(userCompany.Id, userCompany.TeamId);
                //var projects = await _unitOfWork.Repository<JM_ProjectMember>().Include(s => s.JM_Project).Where(s => s.UserId == user.Id).Select(s => _mapper.Map<ProjectResponseItem>(s.JM_Project)).ToListAsync();
                var roles = new List<string>();

                var domain = JsonConvert.SerializeObject(userCompanys.Select(s => s.JM_Company.Organization).ToList());
                var claims = new[]
                {
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(EClaimType.UserId.ToString(), user.Id.ToString()),
                new Claim(EClaimType.AccountCompanyId.ToString(), userCompany.Id.ToString()),
                new Claim(EClaimType.DefaultOrganization.ToString(), userCompany?.JM_Company.Organization ?? String.Empty),
                new Claim(EClaimType.CompanyId.ToString(), userCompany?.CompanyId.ToString()),
                new Claim(EClaimType.TeamId.ToString(), userCompany?.TeamId?.ToString() ?? string.Empty),
                new Claim(EClaimType.Role.ToString(), JsonConvert.SerializeObject(viewPermissions)),
                new Claim(EClaimType.Organization.ToString(), domain),
                new Claim(EClaimType.IsMainAccount.ToString(), userCompany.IsMainAccount.ToString())
            };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Tokens.Key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config.Tokens.Issuer
                    , _config.Tokens.Issuer
                    , claims
                    , expires: DateTime.UtcNow.AddDays(1)
                    , signingCredentials: creds
                    );

                result.IsMainAccount = userCompany.IsMainAccount;
                result.DefaultOrganization = new CompanyResponse
                {
                    Code = userCompany?.JM_Company.Organization,
                    Name = userCompany?.JM_Company.Name,
                    ManagementType = EManagementType.ProjectBasedWork
                };
                result.UserId = user.Id.ToString();
                result.AccountCompanyId = userCompany.Id.ToString();
                result.FullName = user.FullName;
                result.Setting = !string.IsNullOrEmpty(user.Setting) ? JsonConvert.DeserializeObject<SettingResponse>(user.Setting) : new SettingResponse();
                result.Image = user.Image;
                result.ViewPermissions = viewPermissions;
                result.Token = new JwtSecurityTokenHandler().WriteToken(token);
                result.Projects = await GetProjectByUser(userCompany);
            }
            return result;
        }

        public async Task<List<ViewPermissionAction>> GetViewPermissionByUser(Guid accountCompanyId, Guid? teamId)
        {
            var key = _cacheService.GetCacheKey(EControllerKey.Permission, accountCompanyId);
            var permissionFromCacheObject = _cacheService.GetToCache<List<ViewPermissionAction>>(key);
            var viewPermissions = new List<ViewPermissionAction>();
            if (permissionFromCacheObject != null)
            {
                viewPermissions = (List<ViewPermissionAction>)permissionFromCacheObject;
                return viewPermissions;
            }
            viewPermissions = (await _unitOfWork.Repository<SYS_ViewPermissionObject>()
               .Include(s => s.ViewPermission)
               .ThenInclude(s => s.ViewPermissionActions)
               .ThenInclude(s => s.ViewPermissionActionDetails)
               .Where(s => (s.ObjectId == accountCompanyId && s.IsDelete == false && s.ObjectType == EPermissionObject.User) ||
               (teamId == null || (teamId != null && s.ObjectId == teamId.Value && s.ObjectType == EPermissionObject.Team)))
               .SelectMany(s => s.ViewPermission.ViewPermissionActions.Where(u => u.IsDelete == false).Select(d => new
               {
                   View = d.Controller.ToString(),
                   Actions = d.ViewPermissionActionDetails.Select(e => new ViewActionItem
                   {
                       Key = e.Key.ToString(),
                       Value = e.Value != null ? e.Value.Value : false
                   })
               })).ToListAsync()).GroupBy(s => s.View, (key, value) => new ViewPermissionAction
               {
                   View = key.ToString(),
                   Actions = value.SelectMany(e => e.Actions).GroupBy(s => s.Key, (key, value) => new ViewActionItem
                   {
                       Key = key,
                       Value = value.Any(d => d.Value == true) ? true : false
                   }).ToList()
               }).ToList();
            _cacheService.AddToCache(key, viewPermissions);
            return viewPermissions;
        }

        public async Task<bool> CheckPermissionForUser(bool isMainAccount, string controller, string action, ERestMethod method, Guid accountCompanyId, Guid? teamId = null)
        {
            if (isMainAccount)
                return true;
            if (controller.IndexOf('_') >= 0)
            {
                controller = controller.Split('_')[1];
            }
            var result = true;
            var permissions = await GetViewPermissionByUser(accountCompanyId, teamId);
            if (permissions != null && permissions.Count > 0)
            {
                var permissionByController = permissions.Where(s => s.View.ToLower().Equals(controller.ToLower())).FirstOrDefault();
                if (permissionByController == null)
                    return false;

                if (method == ERestMethod.GET)
                {
                    var xx = permissionByController.Actions.Where(s => s.Key.Equals(EActionType.View.ToString()) && s.Value).FirstOrDefault();
                    if (xx == null)
                        return false;
                }
            }
            return result;
        }

        public async Task UpdateUserPermission(List<Guid> userIds, List<Guid> teamIds)
        {
            foreach (var id in userIds)
            {
                _cacheService.RemoveFromCache(_cacheService.GetCacheKey(EControllerKey.Permission, id));
            }
        }

        private async Task<List<ProjectUserResponse>> GetProjectByUser(JM_AccountCompany userCompany)
        {
            var result = new List<ProjectUserResponse>();
            if (userCompany.TeamId != null)
            {
                var projectTeams = await _unitOfWork.Repository<JM_ProjectTeam>()
                    .Where(s => !s.IsDelete && s.TeamId.Equals(userCompany.TeamId.Value) && s.CompanyId == userCompany.CompanyId)
                    .Include(s => s.Project)
                    .Select(s => new ProjectUserResponse
                    {
                        Name = s.Project.Name,
                        Id = s.ProjectId,
                        Code = s.Project.Code
                    }).ToListAsync();
                result.AddRange(projectTeams);
            }

            var projectMembers = await _unitOfWork.Repository<JM_ProjectMember>()
                    .Where(s => !s.IsDelete && s.UserId.Equals(userCompany.UserId) && s.CompanyId == userCompany.CompanyId)
                    .Include(s => s.Project)
                    .Select(s => new ProjectUserResponse
                    {
                        Name = s.Project.Name,
                        Id = s.ProjectId,
                        Code = s.Project.Code
                    }).ToListAsync();

            var projectCreatedByUser = await _unitOfWork.Repository<JM_Project>()
                    .Where(s => !s.IsDelete && s.CreatedUserId.Equals(userCompany.UserId) && s.CompanyId == userCompany.CompanyId)
                    .Select(s => new ProjectUserResponse
                    {
                        Name = s.Name,
                        Id = s.Id,
                        Code = s.Code
                    }).ToListAsync();
            var projectTeamIds = result.Select(s => s.Id);

            result.AddRange(projectMembers.Where(s => !projectTeamIds.Contains(s.Id)));
            var projectIds = result.Select(s => s.Id);
            result.AddRange(projectCreatedByUser.Where(s => !projectIds.Contains(s.Id)));
            return result;
        }
    }
}
