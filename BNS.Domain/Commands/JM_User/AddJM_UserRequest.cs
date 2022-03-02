
using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class AddJM_UserRequest : CommandBase<ApiResult<Guid>>
    {

        [Required]
        public string Token { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [PasswordValidationAttribute]
        public string Password { get; set; }
    }
}
