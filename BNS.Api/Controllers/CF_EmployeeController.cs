
using BNS.Application.Interface;
using BNS.ViewModels;
using BNS.ViewModels.Requests;
using BNS.ViewModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BNS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CF_EmployeeController : BaseController, IApiData<CF_EmployeeModel, CF_EmployeeResponseModel, CF_EmployeeSearchModel>
    {
        private readonly ICF_EmployeeService _EmpService;


        public CF_EmployeeController(IHttpContextAccessor httpContextAccessor,
           ICF_EmployeeService EmpService) : base(httpContextAccessor)
        {
            _EmpService = EmpService;
        }
        [HttpPut("Delete")]
        public async Task<IActionResult> Delete(List<Guid> ids)
        {
            var result = await _EmpService.Delete(ids);
            return Ok(result);
        }

        [HttpPost("GetAllData")]
        public async Task<IActionResult> GetAllData(RequestPageModel<CF_EmployeeSearchModel> model)
        {
            var result = await _EmpService.GetAllData(model);
            return Ok(result);
        }
        [HttpGet("GetByIndex/{id}")]
        public async Task<IActionResult> GetByIndex(Guid id)
        {
            var result = await _EmpService.GetByIndex(id);
            return Ok(result);
        }

        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> Save(CF_EmployeeModel model)
        {
            var result = await _EmpService.Save(model);
            return Ok(result);
        }
    }
}
