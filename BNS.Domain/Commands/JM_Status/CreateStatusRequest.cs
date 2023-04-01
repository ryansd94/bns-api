using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class CreateStatusRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public bool IsStatusStart { get; set; }
        public bool IsStatusEnd { get; set; }
    }
}
