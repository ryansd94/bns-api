using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class CreateTagRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
    }
}
