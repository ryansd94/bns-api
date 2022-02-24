using BNS.Application.Features;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BNS.Api.Controllers.Project
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JM_ProjectController : BaseController
    {
        private IMediator _mediator;
        private readonly ClaimsPrincipal _caller;
        public JM_ProjectController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
            _caller = httpContextAccessor.HttpContext.User;
        }
        [HttpPost]
        public async Task<IActionResult> Save(CreateJM_ProjectCommand.CreateProjectRequest request)
        {
            request.UserId = UserId;
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllData([FromQuery]GetJM_ProjectByUserIdQuery.GetJM_ProjectByUserIdRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

    }
}
