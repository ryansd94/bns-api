using BNS.ViewModels;
using BNS.ViewModels.Requests;
using BNS.ViewModels.Responses.Category;
using System;
using System.Threading.Tasks;

namespace BNS.Application.Interface
{
   public interface ICF_PositionService :
         IDataService<CF_PositionModel, CategoryResponseModel, CategoryResponseModel>
    {
        Task<ApiResult<int>> GetMaxNumber(Guid shopIndex);
    }
}
