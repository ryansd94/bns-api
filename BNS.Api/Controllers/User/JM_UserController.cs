﻿using BNS.Api.Auth;
using BNS.Api.Route;
using BNS.Domain.Commands;
using BNS.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BNS.Api.Controllers.Project
{
    [AppRouteControllerAttribute]
    [ApiController]
    public class JM_UserController : BaseController
    {
        private IMediator _mediator;
        public JM_UserController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpPost("add-user")]
        [BNSAuthorization]
        public async Task<IActionResult> AddUsers(CreateUsersRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet(Name = "get-user")]
        [BNSAuthorization]
        public async Task<IActionResult> GetAllData([FromQuery] GetUserRequest request)
        {
            request.CompanyId = CompanyId;
            request.UserId = UserId;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("status")]
        [BNSAuthorization]
        public async Task<IActionResult> UpdateStatus(UpdateStatusUserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("me/{id}")]
        [BNSAuthorization(false)]
        public async Task<IActionResult> UpdateMe(UpdateMeRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        
        [HttpPut(Name = "delete-user")]
        [BNSAuthorization]
        public async Task<IActionResult> Delete(DeleteUserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
