using BNS.Application.Interface;
using BNS.ViewModels;
using BNS.ViewModels.Requests;
using BNS.ViewModels.Responses;
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
    public class Sys_RoleController : BaseController, IApiData<Sys_RoleModel, Sys_RoleResponseModel, Sys_RoleResponseModel>
    {
        private readonly ISys_RoleService _service;

        public Sys_RoleController(IHttpContextAccessor httpContextAccessor,
            ISys_RoleService RoleService) : base(httpContextAccessor)
        {
            _service = RoleService;
        }

        [HttpPost("Decentralize")]
        public async Task<IActionResult> Decentralize(Sys_DecentralizeModel model)
        {
            model.UserIndex = UserId;
            var result = await _service.Decentralize(model);
            return Ok(result);
        }
        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> Save([FromBody] Sys_RoleModel model)
        {
            model.UserIndex = UserId;
            var result = await _service.Save(model);
            return Ok(result);
        }
        [HttpPost("GetAllData")]
        public async Task<IActionResult> GetAllData(RequestPageModel<Sys_RoleResponseModel> model)
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
    }
}
