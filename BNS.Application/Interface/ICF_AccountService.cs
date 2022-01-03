
using BNS.Data.Entities;
using BNS.ViewModels;
using BNS.ViewModels.Requests;
using BNS.ViewModels.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BNS.Application.Interface
{
    public interface ICF_AccountService : IGenericRepository<CF_Account>
    {

        Task<ApiResult<CF_AccountLoginResponseModel>> Authenticate(CF_AccountLoginModel cF_Account);
        Task<ApiResult<string>> Register(CF_AccountRegisterModel cF_Account);
        ApiResult<CaptChaModel> GetCaptcha();
        Task<ApiResult<string>> SaveBranchDefault(CF_AccountUpdateBranchModel model);
        Task<ApiResult<List<string>>> GetBranchDefault(Guid UserIndex);

    }
}
