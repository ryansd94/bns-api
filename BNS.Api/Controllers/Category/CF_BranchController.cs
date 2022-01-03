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
    public class CF_BranchController : BaseController, IApiData<CF_BranchModel, CF_BranchResponseModel, CF_BranchResponseModel>
    {
        private readonly ICF_BranchService _service;

        public CF_BranchController(IHttpContextAccessor httpContextAccessor,
            ICF_BranchService Service) : base(httpContextAccessor)
        {
            _service = Service;
        }

        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> Save([FromBody] CF_BranchModel model)
        {
            model.UserIndex = UserId;
            var result = await _service.Save(model);
            return Ok(result);
        }
        [HttpPost("GetAllData")]
        public async Task<IActionResult> GetAllData(RequestPageModel<CF_BranchResponseModel> model)
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
        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetByUserId(Guid userId,Guid shopIndex)
        {
            var result = await _service.GetByUserId(userId, shopIndex);
            return Ok(result);
        }
    }
}
