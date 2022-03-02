
using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class ChangePasswordFirstLoginRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Password { get; set; }
    }
}
