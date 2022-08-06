using BNS.Api.Auth;
using BNS.Domain.Commands;
using BNS.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;


namespace BNS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BNSAuthorization]
    public class SYS_FilterController : BaseController
    {
        private IMediator _mediator;
        private readonly ClaimsPrincipal _caller;
        public SYS_FilterController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
            _caller = httpContextAccessor.HttpContext.User;
        }
        [HttpPost]
        public async Task<IActionResult> Save(CreateSYS_FilterRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
