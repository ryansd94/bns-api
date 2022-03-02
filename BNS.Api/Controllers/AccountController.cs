using BNS.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BNS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private IMediator _mediator;
        private readonly ClaimsPrincipal _caller;
        public AccountController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
            _caller = httpContextAccessor.HttpContext.User;
        }

        //[HttpPost("Authenticate")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Authenticate([FromBody] CF_AccountLoginModel model)
        //{
        //    var rs = await _accountService.Authenticate(model);
        //    return Ok(rs);
        //}
        //[HttpPost("Register")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Register([FromBody] CF_AccountRegisterModel model)
        //{
        //    var rs = await _accountService.Register(model);
        //    return Ok(rs);
        //}
        //[HttpGet("GetCaptcha")]
        //[AllowAnonymous]
        //public IActionResult GetCaptcha()
        //{
        //    var rs = _accountService.GetCaptcha();
        //    return Ok(rs);
        //}
        //[HttpPost("SaveBranchDefault")]
        //public async Task<IActionResult> SaveBranchDefault(CF_AccountUpdateBranchModel model)
        //{
        //    var rs = await _accountService.SaveBranchDefault(model);
        //    return Ok(rs);
        //}


        //[HttpGet("GetBranchDefault")]
        //public async Task<IActionResult> GetBranchDefault(Guid UserIndex)
        //{
        //    var rs = await _accountService.GetBranchDefault(UserIndex);
        //    return Ok(rs);
        //}
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
            request.UserId=UserId;
            return Ok(await _mediator.Send(request));
        }
    }
}
