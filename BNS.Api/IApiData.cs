using BNS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.Api
{
    public interface IApiData<SaveModel, GetDataModel, SearchModel>
    {
        Task<IActionResult> GetAllData(RequestPageModel<SearchModel> model);
        Task<IActionResult> GetByIndex(Guid id);
        Task<IActionResult> Save(SaveModel model);
        Task<IActionResult> Delete(List<Guid> ids);
    }
}
