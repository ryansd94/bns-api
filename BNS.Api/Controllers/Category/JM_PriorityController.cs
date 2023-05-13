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
    [BNSAuthorization]
    public class JM_PriorityController : BaseController
    {
        private IMediator _mediator;
        public JM_PriorityController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "save-priority")]
        public async Task<IActionResult> Save(CreatePriorityRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet(Name = "get-priority")]
        public async Task<IActionResult> GetAllData([FromQuery] GetPriorityRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIndex(Guid id)
        {
            var request = new GetPriorityByIdRequest();
            request.Id = id;
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = new DeletePriorityRequest();
            request.ids.Add(id);
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePriorityRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }
    }
}
