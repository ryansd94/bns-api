using BNS.Api.Auth;
using BNS.Api.Route;
using BNS.Domain.Commands;
using BNS.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BNS.Api.Controllers
{
    [AppRouteControllerAttribute]
    [ApiController]
    [BNSAuthorization(false)]
    public class JM_NotifyUserController : BaseController
    {
        private IMediator _mediator;
        public JM_NotifyUserController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "get-notify-user")]
        public async Task<IActionResult> GetAllData([FromQuery] GetNotifyUserRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Read(Guid id, ReadNotifyRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("read-all/{userId}", Name = "read-all")]
        public async Task<IActionResult> ReadAll(ReadAllNotifyRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("delete-read/{userId}", Name = "delete-read")]
        public async Task<IActionResult> DeleteRead(Guid userId, Guid companyId)
        {
            var request = new DeleteReadNotifyRequest();
            request.UserId = userId;
            request.CompanyId = companyId;
            return Ok(await _mediator.Send(request));
        }
    }
}
