using BNS.Api.Auth;
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
    [BNSAuthorization(false)]
    public class JM_TagController : BaseController
    {
        private IMediator _mediator;

        public JM_TagController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }
        
        [HttpPost(Name = "save-tag")]
        public async Task<IActionResult> Save(CreateTagRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        
        [HttpGet(Name = "get-tag")]
        public async Task<IActionResult> GetAllData([FromQuery] GetTagRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIndex(Guid id)
        {
            var request = new GetTagByIdRequest();
            request.Id = id;
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut(Name = "delete-tag")]
        public async Task<IActionResult> Delete(DeleteTagRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTagRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }
    }
}
