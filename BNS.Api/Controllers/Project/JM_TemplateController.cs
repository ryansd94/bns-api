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
    public class JM_TemplateController : BaseController
    {
        private IMediator _mediator;
        public JM_TemplateController(IHttpContextAccessor httpContextAccessor,
            IMediator mediator) : base(httpContextAccessor)
        {
            _mediator = mediator;
        }
        
        [HttpPost(Name = "save-template")]
        public async Task<IActionResult> Save(CreateTemplateRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        
        [HttpGet(Name = "get-template")]
        public async Task<IActionResult> GetAllData([FromQuery] GetTemplateRequest request)
        {
            request.CompanyId = CompanyId;
            request.UserId = UserId;
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIndex(Guid id)
        {
            var request = new GetTemplateByIdRequest();
            request.Id = id;
            request.CompanyId = CompanyId;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTemplateRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }
        
        [HttpPut(Name = "delete-template")]
        public async Task<IActionResult> Delete(DeleteTemplateRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
