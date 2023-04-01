using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class UpdateMeRequest: CommandBase<ApiResult<Guid>>
    {
        [Required]
        public Guid Id { get; set; }
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
