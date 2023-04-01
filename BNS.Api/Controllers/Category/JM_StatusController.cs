using BNS.Api.Auth;
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
    public class JM_StatusController : BaseController
    {
        private IMediator _mediator;
        private readonly ClaimsPrincipal _caller;
        public JM_StatusController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
            _caller = httpContextAccessor.HttpContext.User;
        }

        [HttpPost]
        public async Task<IActionResult> Save(CreateStatusRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("check")]
        public async Task<IActionResult> CheckStatus([FromQuery] GetCheckStatusRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllData([FromQuery] GetStatusRequest request)
        {
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = new DeleteStatusRequest();
            request.ids.Add(id);
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
