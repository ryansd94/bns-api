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
    [BNSAuthorization(false)]
    public class JM_CommentController : BaseController
    {
        private IMediator _mediator;
        public JM_CommentController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }
        
        [HttpPost(Name = "save-comment")]
        public async Task<IActionResult> Comment(CreateCommentRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("children")]
        public async Task<IActionResult> GetChildrenComment([FromQuery] GetCommentRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut(Name = "delete-comment")]
        public async Task<IActionResult> Delete(DeleteCommentRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCommentRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }
    }
}
