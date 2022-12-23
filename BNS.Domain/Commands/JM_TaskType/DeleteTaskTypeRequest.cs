using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class DeleteTaskTypeRequest : CommandBase<ApiResult<Guid>>
    {
        public DeleteTaskTypeRequest()
        {
            ids = new List<Guid>();
        }
        [Required]
        public List<Guid> ids { get; set; }
    }
}
