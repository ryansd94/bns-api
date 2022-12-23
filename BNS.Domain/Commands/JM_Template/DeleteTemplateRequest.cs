using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class DeleteTemplateRequest : CommandBase<ApiResult<Guid>>
    {
        public DeleteTemplateRequest()
        {
            ids = new List<Guid>();
        }
        [Required]
        public List<Guid> ids { get; set; }
    }
}
