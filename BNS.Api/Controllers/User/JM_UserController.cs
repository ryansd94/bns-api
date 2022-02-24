﻿using BNS.Application.Features;
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
        [Authorize]
        public async Task<IActionResult> SendMailAddUser( SendMailAddJM_UserCommand.SendMailAddJM_UserCommandRequest request)
        {
            request.UserId = UserId;
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
        public async Task<IActionResult> Signup(AddJM_UserCommand.AddJM_UserCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllData([FromQuery] GetJM_UserQuery.GetJM_UserRequest request)
        {
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("status")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateStatus(UpdateStatusJM_UserCommand.UpdateStatusJM_UserRequest request)
        {
            request.CompanyId = CompanyId;
            request.UserId = UserId;
            return Ok(await _mediator.Send(request));
        }

    }
}
