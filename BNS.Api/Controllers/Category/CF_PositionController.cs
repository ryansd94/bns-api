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

    public class CF_PositionController : BaseController, IApiData<CF_PositionModel, CategoryResponseModel, CategoryResponseModel>
    {
        private readonly ICF_PositionService _service;

        public CF_PositionController(IHttpContextAccessor httpContextAccessor,
            ICF_PositionService service) : base(httpContextAccessor)
        {
            _service = service;
        }

        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> Save([FromBody] CF_PositionModel model)
        {
            model.UserIndex = UserId;
            var result = await _service.Save(model);
            return Ok(result);
        }
        [HttpPost("GetAllData")]
        public async Task<IActionResult> GetAllData(RequestPageModel<CategoryResponseModel> model)
        {
            var result = await _service.GetAllData(model);
            return Ok(result);
        }

        [HttpGet("GetByIndex/{id}")]
        public async Task<IActionResult> GetByIndex(Guid id)
        {
            var result = await _service.GetByIndex(id);
            return Ok(result);
        }

        [HttpPut("Delete")]
        public async Task<IActionResult> Delete(List<Guid> ids)
        {
            var result = await _service.Delete(ids);
            return Ok(result);
        }
        [HttpGet("GetMaxNumber")]
        public async Task<IActionResult> GetMaxNumber(Guid shopIndex)
        {
            var result = await _service.GetMaxNumber(shopIndex);
            return Ok(result);
        }
    }
}
