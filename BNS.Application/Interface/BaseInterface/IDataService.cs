using BNS.ViewModels;
using BNS.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BNS.Application.Interface
{
    public interface IDataService<SaveModel, GetDataModel,SearchModel>
    {
        Task<ApiResult<List<GetDataModel>>> GetAllData(RequestPageModel<SearchModel> model);
        Task<ApiResult<GetDataModel>> GetByIndex(Guid id);
        Task<ApiResult<string>> Save(SaveModel model);
        Task<ApiResult<string>> Delete (List<Guid> ids);

        
    }
}
