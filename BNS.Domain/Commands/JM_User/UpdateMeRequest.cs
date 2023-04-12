using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class UpdateMeRequest: CommandBase<ApiResult<Guid>>
    {
        [Required]
        public Guid Id { get; set; }
        public List<UpdateMeItem> Configs { get; set; }
    }

    public class UpdateMeItem
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
