using BNS.ViewModels;
using BNS.ViewModels.Requests;
using BNS.ViewModels.Responses.Category;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BNS.Application.Interface
{
    public interface ICF_BranchService :
        IDataService<CF_BranchModel, CF_BranchResponseModel, CF_BranchResponseModel>
    {
        Task<ApiResult<int>> GetMaxNumber(Guid shopIndex);

        Task<ApiResult<List<CF_BranchDefaultResponseModel>>> GetByUserId(Guid UserId,
            Guid ShopIndex);
    }
}
