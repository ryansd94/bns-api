using BNS.Api.Auth;
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
    [BNSAuthorization]
    public class JM_SprintController : BaseController
    {

        private IMediator _mediator;
        public JM_SprintController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "save-sprint")]
        public async Task<IActionResult> Save(CreateJM_SprintRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet(Name = "get-sprint")]
        public async Task<IActionResult> GetAllData([FromQuery] GetJM_SprintRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIndex(Guid id)
        {
            var request = new GetTeamByIdRequest();
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = new DeleteJM_SprintRequest();
            request.ids.Add(id);
            return Ok(await _mediator.Send(request));
        }

        [HttpPut(Name = "update-sprint")]
        public async Task<IActionResult> Update(UpdateJM_SprintRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

    }
}
