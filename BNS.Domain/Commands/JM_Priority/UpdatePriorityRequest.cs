using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class UpdatePriorityRequest : CommandUpdateBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
        public string Color { get; set; }
        public int Order { get; set; }
    }
}
