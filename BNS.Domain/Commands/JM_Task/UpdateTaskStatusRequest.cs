using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class UpdateTaskStatusRequest: CommandUpdateBase<ApiResult<Guid>>
    {
        [Required]
        public Guid StatusId { get; set; }
    }
}
