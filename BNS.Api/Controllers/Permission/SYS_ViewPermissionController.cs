using BNS.Api.Auth;
using BNS.Api.Route;
using BNS.Domain.Commands;
using BNS.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BNS.Api.Controllers.Permission
{
    [AppRouteControllerAttribute]
    [ApiController]
    public class SYS_ViewPermissionController : BaseController
    {
        private IMediator _mediator;
        public SYS_ViewPermissionController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "save-view-permission")]
        [BNSAuthorization]
        public async Task<IActionResult> Comment(CreateViewPermissionRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet(Name = "get-view-permission")]
        [BNSAuthorization]
        public async Task<IActionResult> GetAllData([FromQuery] GetViewPermissionRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        [BNSAuthorization]
        public async Task<IActionResult> GetByIndex(Guid id)
        {
            var request = new GetViewPermissionByIdRequest();
            request.Id = id;
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("users", Name = "get-user-view-permission")]
        [BNSAuthorization(false)]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetUserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("teams", Name = "get-team-view-permission")]
        [BNSAuthorization(false)]
        public async Task<IActionResult> GetAllTeams([FromQuery] GetTeamRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
