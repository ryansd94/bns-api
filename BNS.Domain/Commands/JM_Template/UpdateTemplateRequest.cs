using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class UpdateTemplateRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public JM_ColumnItemRoot Content { get; set; }
        public List<StatusItem> Status { get; set; }
    }
}
