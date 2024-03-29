﻿using BNS.Api.Auth;
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
    [BNSAuthorization]
    public class JM_StatusController : BaseController
    {
        private IMediator _mediator;
        public JM_StatusController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "save-status")]
        public async Task<IActionResult> Save(CreateStatusRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("check")]
        public async Task<IActionResult> CheckStatus([FromQuery] GetCheckStatusRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet(Name = "get-status")]
        public async Task<IActionResult> GetAllData([FromQuery] GetStatusRequest request)
        {
            request.CompanyId = CompanyId;
            request.UserId = UserId;
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIndex(Guid id)
        {
            var request = new GetStatusByIdRequest();
            request.Id = id;
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut(Name = "delete-status")]
        public async Task<IActionResult> Delete(DeleteStatusRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateStatusRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }
    }
}
