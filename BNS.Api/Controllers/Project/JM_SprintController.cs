using BNS.Application.Features;
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
    [Authorize]
    public class JM_SprintController : BaseController
    {

        private IMediator _mediator;
        private readonly ClaimsPrincipal _caller;
        public JM_SprintController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
            _caller = httpContextAccessor.HttpContext.User;
        }
        [HttpPost]
        public async Task<IActionResult> Save(CreateJM_SprintCommand.CreateJM_SprintRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllData([FromQuery] GetJM_SprintQuery.GetJM_SprintRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIndex(Guid id)
        {
            var request = new GetJM_TeamByIdQuery.GetJM_TeamByIdRequest();
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = new DeleteJM_SprintCommand.DeleteJM_SprintRequest();
            request.ids.Add(id);
            return Ok(await _mediator.Send(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateJM_SprintCommand.UpdateJM_SprintRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

    }
}
