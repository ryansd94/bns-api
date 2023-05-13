using BNS.Data.Entities.JM_Entities;
using BNS.Domain.Commands;
using BNS.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Interface
{
    public interface IAccountService
    {
        Task<LoginResponse> GetUserLoginInfo(JM_Account user);
        Task<List<ViewPermissionAction>> GetViewPermissionByUser(Guid accountCompanyId, Guid? teamId);
        Task<bool> CheckPermissionForUser(bool isMainAccount, string controller, string action, ERestMethod method, Guid accountCompanyId, Guid? teamId = null);
        Task UpdateUserPermission(List<Guid> userIds, List<Guid> teamIds);
    }
}
