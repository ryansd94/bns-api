using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands 
{
    public class DeleteUserRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public List<Guid> Ids { get; set; } = new List<Guid>();
    }
}
