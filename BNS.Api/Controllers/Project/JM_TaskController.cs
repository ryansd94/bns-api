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
    public class JM_TaskController : BaseController
    {
        private IMediator _mediator;
        public JM_TaskController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "save-task")]
        [BNSAuthorization]
        public async Task<IActionResult> Save(CreateTaskRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("change-task-type/{id}")]
        [BNSAuthorization]
        public async Task<IActionResult> ChangeTaskType(Guid id, ChangeTaskTypeRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("status/{id}")]
        [BNSAuthorization]
        public async Task<IActionResult> UpdateStatus(Guid id, UpdateTaskStatusRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("{id}")]
        [BNSAuthorization]
        public async Task<IActionResult> Update(Guid id, UpdateTaskRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }

        [HttpGet(Name = "get-task")]
        [BNSAuthorization]
        public async Task<IActionResult> GetAllData([FromQuery] GetTaskRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("user-assign")]
        [BNSAuthorization(false)]
        public async Task<IActionResult> GetUserAssign([FromQuery] GetUserAssignRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        [BNSAuthorization]
        public async Task<IActionResult> GetByIndex(Guid id)
        {
            var request = new GetTaskByIdRequest();
            request.Id = id;
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("user-suggest", Name = "get-user-suggest-task")]
        [BNSAuthorization(false)]
        public async Task<IActionResult> GetSuggest([FromQuery] GetUserSuggest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
