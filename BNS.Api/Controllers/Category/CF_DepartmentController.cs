using BNS.Application.Interface;
using BNS.ViewModels;
using BNS.ViewModels.Requests;
using BNS.ViewModels.Responses.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BNS.Api.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CF_DepartmentController : BaseController, IApiData<CF_DepartmentModel, CategoryResponseModel, CategoryResponseModel>
    {
        private readonly ICF_DepartmentService _DeptService;

        public CF_DepartmentController(IHttpContextAccessor httpContextAccessor,
            ICF_DepartmentService DeptService) : base(httpContextAccessor)
        {
            _DeptService = DeptService;
        }

        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> Save([FromBody] CF_DepartmentModel model)
        {
            model.UserIndex = UserId;
            var result = await _DeptService.Save(model);
            return Ok(result);
        }
        [HttpPost("GetAllData")]
        public async Task<IActionResult> GetAllData(RequestPageModel<CategoryResponseModel> model)
        {
            var result = await _DeptService.GetAllData(model);
            return Ok(result);
        }

        [HttpGet("GetByIndex/{id}")]
        public async Task<IActionResult> GetByIndex(Guid id)
        {
            var result = await _DeptService.GetByIndex(id);
            return Ok(result);
        }

        [HttpPut("Delete")]
        public async Task<IActionResult> Delete(List<Guid> ids)
        {
            var result = await _DeptService.Delete(ids);
            return Ok(result);
        }
        [HttpGet("GetMaxNumber")]
        public async Task<IActionResult> GetMaxNumber(Guid shopIndex)
        {
            var result = await _DeptService.GetMaxNumber(shopIndex);
            return Ok(result);
        }
    }
}
