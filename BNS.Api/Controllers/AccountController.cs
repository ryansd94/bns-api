using BNS.Domain.Commands;
using BNS.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BNS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private IMediator _mediator;
        public AccountController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpPost("validate-signup")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateSignup(ValidateSignupRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        
        [HttpPost("validate-join")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateJoin(ValidateAddUserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup(AddUserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("login-google")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithGoogle(LoginGoogleRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("register-google")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterWithGoogle(RegisterGoogleRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("password-first")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordFirst(ChangePasswordFirstLoginRequest request)
        {
            request.UserId = UserId;
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("check-organization")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckOrganization(CheckOrganizationRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
