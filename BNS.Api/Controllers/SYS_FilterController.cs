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
    public class SYS_FilterController : BaseController
    {
        private IMediator _mediator;

        public SYS_FilterController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }
        
        [HttpPost(Name = "save-filter")]
        public async Task<IActionResult> Save(CreateSYS_FilterRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        
        [HttpGet(Name = "get-filter")]
        public async Task<IActionResult> GetAllData([FromQuery] GetSYS_FilterConfigRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var request = new GetSYS_FilterConfigByIdRequest();
            request.Id = id;
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }
    }
}
