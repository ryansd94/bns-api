using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class CreatePriorityRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
        public string Color { get; set; }
        public int Order { get; set; }
    }
}
