using BNS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class UpdateJM_TeamRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public List<Guid> Members { get; set; }
    }
}
