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
using static BNS.Utilities.Enums;

namespace BNS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Sys_RoleClaimController : BaseController
    {
        private readonly ISys_RoleClaimService _service;

        public Sys_RoleClaimController(IHttpContextAccessor httpContextAccessor,
            ISys_RoleClaimService RoleService) : base(httpContextAccessor)
        {
            _service = RoleService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(Guid id, ERoleType type)
        {
            var result = await _service.GetByDataId(id, type);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Save(Sys_RoleClaimModel model)
        {
            model.UserIndex = UserId;
            var result = await _service.Save(model);
            return Ok(result);
        }
    }
}
