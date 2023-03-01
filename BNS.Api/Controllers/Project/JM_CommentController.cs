using BNS.Api.Auth;
using BNS.Domain.Commands;
using BNS.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BNS.Api.Controllers.Project
{
    [Route("api/[controller]")]
    [ApiController]
    [BNSAuthorization]
    public class JM_CommentController : BaseController
    {
        private IMediator _mediator;
        public JM_CommentController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Comment(CreateCommentRequest request)
        {
            return Ok(await _mediator.Send(request));
        }


        [HttpGet("children")]
        public async Task<IActionResult> GetChildrenComment([FromQuery] GetCommentRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = new DeleteCommentRequest();
            request.ids.Add(id);
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
