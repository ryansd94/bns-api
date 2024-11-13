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
    public class JM_TaskCalendarController : BaseController
    {
        private IMediator _mediator;
        public JM_TaskCalendarController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "get-task-calendar")]
        [BNSAuthorization]
        public async Task<IActionResult> GetAllData([FromQuery] GetTaskCalendarRequest request)
        {
            request.CompanyId = CompanyId;
            request.UserId = UserId;
            return Ok(await _mediator.Send(request));
        }
    }
}
