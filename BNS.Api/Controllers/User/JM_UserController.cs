﻿using BNS.Api.Auth;
using BNS.Domain.Commands;
using BNS.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BNS.Api.Controllers.Project
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
        public async Task<IActionResult> SendMailAddUser(SendMailAddUserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("validate-signup")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateSignup(ValidateAddUserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup(AddUserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        [BNSAuthorization]
        public async Task<IActionResult> GetAllData([FromQuery] GetUserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        
        [HttpGet("suggest")]
        [BNSAuthorization]
        public async Task<IActionResult> GetSuggest([FromQuery] GetUserSuggest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("status")]
        [BNSAuthorization]
        public async Task<IActionResult> UpdateStatus(UpdateStatusUserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        
        [HttpPut("me/{id}")]
        [BNSAuthorization]
        public async Task<IActionResult> UpdateMe(UpdateMeRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("{id}")]
        [BNSAuthorization]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = new DeleteUserRequest();
            request.Ids.Add(id);
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }


    }
}
