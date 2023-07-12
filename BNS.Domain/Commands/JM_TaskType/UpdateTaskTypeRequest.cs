using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class UpdateTaskTypeRequest : CommandUpdateBase<ApiResult<Guid>>
    {
    }
}
