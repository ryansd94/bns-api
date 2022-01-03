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

namespace BNS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CF_AreaController : BaseController, IApiData<CF_AreaModel, CategoryResponseModel, CategoryResponseModel>
    {
        private readonly ICF_AreaService _AreaService;

        public CF_AreaController(IHttpContextAccessor httpContextAccessor, 
            ICF_AreaService AreaService) : base(httpContextAccessor)
        {
            _AreaService = AreaService;
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] CF_AreaModel model)
        {
            model.UserIndex = UserId;
            var result = await _AreaService.Save(model);
            return Ok(result);
        }
        [HttpPost("GetAllData")]
        public async Task<IActionResult> GetAllData(RequestPageModel<CategoryResponseModel> model)
        {
            var result = await _AreaService.GetAllData(model);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIndex(Guid id)
        {
            var result = await _AreaService.GetByIndex(id);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(List<Guid> ids)
        {
            var result = await _AreaService.Delete(ids);
            return Ok(result);
        }
        [HttpGet("max-number")]
        public async Task<IActionResult> GetMaxNumber(Guid shopIndex, Guid? branchIndex)
        {
            var result = await _AreaService.GetMaxNumber(shopIndex,branchIndex);
            return Ok(result);
        }
    }
}
