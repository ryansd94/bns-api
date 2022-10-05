﻿using BNS.Api.Auth;
using BNS.Domain.Commands;
using BNS.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BNS.Api.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    [BNSAuthorization]
    public class JM_TeamController : BaseController
    {
        private IMediator _mediator;
        private readonly ClaimsPrincipal _caller;
        public JM_TeamController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
            _caller = httpContextAccessor.HttpContext.User;
        }
        [HttpPost]
        public async Task<IActionResult> Save(CreateTeamRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllData([FromQuery] GetJM_TeamRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var request = new GetJM_TeamByIdRequest();
            request.Id = id;
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = new DeleteTeamRequest();
            request.ids.Add(id);
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTeamRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("member/{id}")]
        public async Task<IActionResult> UpdateMember(Guid id, UpdateMemberTeamRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("member/{id}")]
        public async Task<IActionResult> DeleteMember(Guid id, DeleteMemberTeamRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }
    }
}
