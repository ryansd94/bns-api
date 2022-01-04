using BNS.Application.Features;
using BNS.ViewModels;
using BNS.ViewModels.Requests;
using BNS.ViewModels.Responses.Category;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllData(GetJM_TeamQuery.GetJM_TeamRequest request)
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


        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteJM_TeamCommand.DeleteJM_TeamRequest request)
        {
            request.CreatedBy = UserId;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateJM_TeamCommand.UpdateJM_TeamRequest request)
        {
            request.CreatedBy = UserId;
            return Ok(await _mediator.Send(request));
        }

    }
}
