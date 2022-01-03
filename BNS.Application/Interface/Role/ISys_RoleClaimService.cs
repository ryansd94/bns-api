using BNS.ViewModels;
using BNS.ViewModels.Requests;
using BNS.ViewModels.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Application.Interface
{
    public interface ISys_RoleClaimService 
    {
        Task<ApiResult<Sys_RoleClaimResponseModel>> GetByDataId(Guid id, ERoleType type);
        Task<ApiResult<string>> Save(Sys_RoleClaimModel model);
    }
}
