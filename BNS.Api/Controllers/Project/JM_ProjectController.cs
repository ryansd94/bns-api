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
    public class JM_ProjectController : BaseController
    {
        private IMediator _mediator;
        public JM_ProjectController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "save-project")]
        public async Task<IActionResult> Save(CreateProjectRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet(Name = "get-project")]
        public async Task<IActionResult> GetAllData([FromQuery] GetProjectByUserIdRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIndex(Guid id)
        {
            var request = new GetProjectByIdRequest();
            request.Id = id;
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateProjectRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }
    }
}
