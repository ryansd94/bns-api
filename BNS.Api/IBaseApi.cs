using BNS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace BNS.Api
{
    public interface IBaseApi<SaveModel, UpdateModel, DeleteModel, SearchModel>
    {
        Task<IActionResult> GetByIndex(Guid id);
        Task<IActionResult> Save(SaveModel request);
        Task<IActionResult> Update(UpdateModel request);
        Task<IActionResult> Delete(DeleteModel request);
    }
}
