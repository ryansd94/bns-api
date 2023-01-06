using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class UpdateTagRequest : CommandUpdateBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
    }
}
