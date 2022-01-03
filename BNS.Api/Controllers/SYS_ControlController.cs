using BNS.Application.Interface;
using BNS.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BNS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SYS_ControlController : BaseController
    {
        private readonly ISYS_ControlService _service;


        public SYS_ControlController(IHttpContextAccessor httpContextAccessor,
          ISYS_ControlService service) : base(httpContextAccessor)
        {
            _service = service;
        }

        [HttpPost("GetFieldControl")]
        public async Task<IActionResult> GetFieldControl(SYS_ControlModel model)
        {
            var result = await _service.GetFieldControl(model);
            return Ok(result);
        }

        [HttpPost("GetColumnControl")]
        public async Task<IActionResult> GetColumnControl(SYS_ControlModel model)
        {
            var result = await _service.GetColumnControl(model);
            return Ok(result);
        }



        

        [HttpPost("SaveFieldControl")]
        public async Task<IActionResult> SaveFieldControl(SYS_FieldModel model)
        {
            var result = await _service.SaveFieldControl(model);
            return Ok(result);
        }
    }
}
