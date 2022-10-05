using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class DeleteTaskRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public List<Guid> ids { get; set; }
    }
}
