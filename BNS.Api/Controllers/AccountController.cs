using BNS.Application.Features;
using BNS.Application.Interface;
using BNS.ViewModels.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using static BNS.Application.Features.LoginGoogleCommand;

namespace BNS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly ICF_AccountService _accountService;
        private IMediator _mediator;
        private readonly ClaimsPrincipal _caller;
        public AccountController(IHttpContextAccessor httpContextAccessor,
           ICF_AccountService iCF_AccountService,
            IMediator mediator) : base(httpContextAccessor)
        {
            _accountService = iCF_AccountService;
            _mediator = mediator;
            _caller = httpContextAccessor.HttpContext.User;
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] CF_AccountLoginModel model)
        {
            var rs = await _accountService.Authenticate(model);
            return Ok(rs);
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CF_AccountRegisterModel model)
        {
            var rs = await _accountService.Register(model);
            return Ok(rs);
        }
        [HttpGet("GetCaptcha")]
        [AllowAnonymous]
        public IActionResult GetCaptcha()
        {
            var rs = _accountService.GetCaptcha();
            return Ok(rs);
        }
        [HttpPost("SaveBranchDefault")]
        public async Task<IActionResult> SaveBranchDefault(CF_AccountUpdateBranchModel model)
        {
            var rs = await _accountService.SaveBranchDefault(model);
            return Ok(rs);
        }


        [HttpGet("GetBranchDefault")]
        public async Task<IActionResult> GetBranchDefault(Guid UserIndex)
        {
            var rs = await _accountService.GetBranchDefault(UserIndex);
            return Ok(rs);
        }
        [HttpPost("login-google")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginGoogle(LoginGoogleRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut("password-first")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordFirst(ChangePasswordFirstLoginCommand.ChangePasswordFirstLoginRequest request)
        {
            request.CreatedBy=UserId;
            return Ok(await _mediator.Send(request));
        }
    }
}
