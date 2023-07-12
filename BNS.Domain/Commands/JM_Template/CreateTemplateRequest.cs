using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class CreateTemplateRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ColumnItemRoot Content { get; set; }
        public List<StatusItemRequest> Status { get; set; }
    }

    public class StatusItemRequest
    {
        public Guid Id { get; set; }
    }
}
