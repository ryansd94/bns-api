using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class ChangeTaskTypeRequest: CommandUpdateBase<ApiResult<Guid>>
    {
        [Required]
        public Guid TaskTypeId { get; set; }
    }
}
