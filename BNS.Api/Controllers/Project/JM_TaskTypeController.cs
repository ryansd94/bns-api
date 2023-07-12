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
    public class JM_TaskTypeController : BaseController
    {
        private IMediator _mediator;
        public JM_TaskTypeController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "save-tasktype")]
        [BNSAuthorization]
        public async Task<IActionResult> Save(CreateTaskTypeRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet(Name = "get-tasktype")]
        [BNSAuthorization(false)]
        public async Task<IActionResult> GetAllData([FromQuery] GetTaskTypeRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("{id}")]
        [BNSAuthorization]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = new DeleteTaskTypeRequest();
            request.Ids.Add(id);
            request.CompanyId = CompanyId;
            request.UserId = UserId;
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

        [HttpPut("{id}")]
        [BNSAuthorization]
        public async Task<IActionResult> Update(Guid id, UpdateTaskTypeRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }
    }
}
