using BNS.ViewModels;
using BNS.ViewModels.Requests;
using BNS.ViewModels.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Application.Interface
{
    public interface ISys_RoleService :
        IDataService<Sys_RoleModel, Sys_RoleResponseModel, Sys_RoleResponseModel>
    {
        Task<ApiResult<string>> Decentralize(Sys_DecentralizeModel model);
    }
}
