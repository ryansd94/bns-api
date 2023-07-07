using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class UpdateProjectRequest : CommandUpdateBase<ApiResult<Guid>>
    {
    }
}
