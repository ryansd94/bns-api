using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class UpdateStatusRequest : CommandUpdateBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsStatusStart { get; set; }
        public bool IsStatusEnd { get; set; }
    }
}
