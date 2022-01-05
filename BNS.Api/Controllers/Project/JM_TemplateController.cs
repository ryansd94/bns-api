using BNS.Application.Features;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BNS.Api.Controllers.Project
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JM_TemplateController : BaseController
    {
        private IMediator _mediator;
        private readonly ClaimsPrincipal _caller;
        public JM_TemplateController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
            _caller = httpContextAccessor.HttpContext.User;
        }
        [HttpPost]
        public async Task<IActionResult> Save(CreateJM_TemplateCommand.CreateJM_TemplateRequest request)
        {
            request.CreatedBy = UserId;
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllData(GetJM_TemplateQuery.GetJM_TemplateRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIndex(Guid id)
        {
            var request = new GetJM_TemplateByIdQuery.GetJM_TemplateByIdRequest();
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }
    }
}
