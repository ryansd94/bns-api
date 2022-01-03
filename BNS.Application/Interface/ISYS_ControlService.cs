using BNS.ViewModels;
using BNS.ViewModels.Requests;
using BNS.ViewModels.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BNS.Application.Interface
{
    public interface ISYS_ControlService
    {
        Task<ApiResult<SYS_FieldControlResponseModel>> GetFieldControl(SYS_ControlModel model);

        Task<ApiResult<SYS_FieldControlResponseModel>> SaveFieldControl(SYS_FieldModel model);
        Task<ApiResult<SYS_ColumnControlResponseModel>> GetColumnControl(SYS_ControlModel model);
        Task<ApiResult<SYS_ColumnControlResponseModel>> SaveColumnControl(SYS_FieldModel model);

        Task<ApiResult<List<SYS_ColumnControlResponseModel>>> GetColumnControl(Guid shopIndex);

    }
}
