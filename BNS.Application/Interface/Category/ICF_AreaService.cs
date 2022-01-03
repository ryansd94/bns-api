using BNS.ViewModels;
using BNS.ViewModels.Requests;
using BNS.ViewModels.Responses.Category;
using System;
using System.Threading.Tasks;

namespace BNS.Application.Interface
{
    public interface ICF_AreaService :
        IDataService<CF_AreaModel, CategoryResponseModel, CategoryResponseModel>
    {
        Task<ApiResult<int>> GetMaxNumber(Guid shopIndex, Guid? branchIndex);
    }
}
