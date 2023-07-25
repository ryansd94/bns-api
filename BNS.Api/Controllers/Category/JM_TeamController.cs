using BNS.Api.Auth;
using BNS.Api.Route;
using BNS.Domain.Commands;
using BNS.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BNS.Api.Controllers.Category
{
    [AppRouteControllerAttribute]
    [ApiController]
    public class JM_TeamController : BaseController
    {
        private IMediator _mediator;
        public JM_TeamController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "save-team")]
        [BNSAuthorization]
        public async Task<IActionResult> Save(CreateTeamRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet(Name = "get-team")]
        [BNSAuthorization]
        public async Task<IActionResult> GetAllData([FromQuery] GetTeamRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        [BNSAuthorization]
        public async Task<IActionResult> GetById(Guid id)
        {
            var request = new GetTeamByIdRequest();
            request.Id = id;
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("{id}")]
        [BNSAuthorization]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = new DeleteTeamRequest();
            request.ids.Add(id);
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("{id}")]
        [BNSAuthorization]
        public async Task<IActionResult> Update(Guid id, UpdateTeamRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("member/{id}")]
        [BNSAuthorization]
        public async Task<IActionResult> UpdateMember(Guid id, UpdateMemberTeamRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("member/{id}")]
        [BNSAuthorization]
        public async Task<IActionResult> DeleteMember(Guid id, DeleteMemberTeamRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("users", Name = "get-user-team")]
        [BNSAuthorization(false)]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetUserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
