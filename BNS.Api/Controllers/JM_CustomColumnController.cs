using BNS.Api.Auth;
using BNS.Api.Route;
using BNS.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BNS.Api.Controllers
{
    [AppRouteControllerAttribute]
    [ApiController]
    [BNSAuthorization(false)]
    public class JM_CustomColumnController : BaseController
    {
        private IMediator _mediator;
        public JM_CustomColumnController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }
        
        [HttpGet(Name = "get-custom-column")]
        public async Task<IActionResult> GetAllData([FromQuery] GetCustomColumnRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
