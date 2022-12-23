using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class UpdateStatusRequest : CommandUpdateBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
