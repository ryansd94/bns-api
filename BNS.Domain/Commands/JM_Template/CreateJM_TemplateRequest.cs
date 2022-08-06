using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Commands
{
    public class CreateJM_TemplateRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public List<StatusItem> Status { get; set; }
    }

    public class StatusItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsNew { get; set; }
    }
}
