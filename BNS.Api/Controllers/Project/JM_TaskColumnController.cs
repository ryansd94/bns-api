using BNS.Api.Auth;
using BNS.Api.Route;
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
    public class JM_TaskColumnController : BaseController
    {
        private IMediator _mediator;
        public JM_TaskColumnController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "get-taskcolumn")]
        [BNSAuthorization(false)]
        public async Task<IActionResult> GetAllData([FromQuery] GetCustomColumnsRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        [BNSAuthorization]
        public async Task<IActionResult> GetByIndex(Guid id)
        {
            var request = new GetTaskTypeByIdRequest();
            request.Id = id;
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }
    }
}
