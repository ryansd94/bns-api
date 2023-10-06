using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain
{
    public class CommandDeleteRequest: CommandBase<ApiResult<Guid>>
    {
        public CommandDeleteRequest()
        {
            Ids = new List<Guid>();
        }
        [Required]
        public List<Guid> Ids { get; set; }
    }
}
