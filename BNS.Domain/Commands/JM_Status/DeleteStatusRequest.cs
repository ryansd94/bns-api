using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class DeleteStatusRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public List<Guid> ids { get; set; } = new List<Guid>();
    }
}
