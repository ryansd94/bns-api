using BNS.Api.Auth;
using BNS.Domain.Commands;
using BNS.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BNS.Api.Controllers.Project
{
    [Route("api/[controller]")]
    [ApiController]
    [BNSAuthorization]
    public class JM_TaskController : BaseController
    {
        private IMediator _mediator;
        private readonly ClaimsPrincipal _caller;
        public JM_TaskController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
            _caller = httpContextAccessor.HttpContext.User;
        }

        [HttpPost]
        public async Task<IActionResult> Save(CreateTaskRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("change-task-type/{id}")]
        public async Task<IActionResult> ChangeTaskType(Guid id,ChangeTaskTypeRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTaskRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllData([FromQuery] GetTaskRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("user-assign")]
        public async Task<IActionResult> GetUserAssign([FromQuery] GetUserAssignRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIndex(Guid id)
        {
            var request = new GetTaskByIdRequest();
            request.Id = id;
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }
    }
}
