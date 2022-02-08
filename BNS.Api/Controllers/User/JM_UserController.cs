using BNS.Application.Features;
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
        [HttpPut("send-mail-add-user/{teamId}")]
        [Authorize]
        public async Task<IActionResult> SendMailAddUser(Guid teamId, SendMailAddJM_UserCommand.SendMailAddJM_UserCommandRequest request)
        {
            request.CreatedBy = UserId;
            request.TeamId = teamId;
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("validate-signup")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateSignup(ValidateAddJM_UserCommnad.ValidateAddJM_UserCommnadRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup(ValidateAddJM_UserCommnad.ValidateAddJM_UserCommnadRequest request)
        {
            return Ok(await _mediator.Send(request));
        }


    }
}
