using BNS.Api.Auth;
using BNS.Domain.Commands;
using BNS.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BNS.Api.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class JM_UserController : BaseController
    {
        private IMediator _mediator;
        private readonly ClaimsPrincipal _caller;
        public JM_UserController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
            _caller = httpContextAccessor.HttpContext.User;
        }
        [HttpPost("add-user")]
        [BNSAuthorization]
        public async Task<IActionResult> SendMailAddUser(  SendMailAddJM_UserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("validate-signup")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateSignup( ValidateAddJM_UserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup( AddJM_UserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        [BNSAuthorization]
        public async Task<IActionResult> GetAllData([FromQuery]  GetJM_UserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("status")]
        [BNSAuthorization]
        public async Task<IActionResult> UpdateStatus( UpdateStatusJM_UserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("{id}")]
        [BNSAuthorization]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = new DeleteJM_UserRequest();
            request.Ids.Add(id);
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }


    }
}
