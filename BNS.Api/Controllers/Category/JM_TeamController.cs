using BNS.Application.Features;
using BNS.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BNS.Api.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> Save(CreateJM_TeamCommand.CreateTeamRequest request)
        {
            request.CreatedBy = UserId;
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllData([FromQuery]GetJM_TeamQuery.GetJM_TeamRequest request)
        {
            request.CompanyId = CompanyId;
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
            var request = new DeleteJM_TeamCommand.DeleteJM_TeamRequest();
            request.ids.Add(id);
            request.CreatedBy = UserId;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,UpdateJM_TeamCommand.UpdateJM_TeamRequest request)
        {
            request.CreatedBy = UserId;
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }

    }
}
