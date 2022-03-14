
using BNS.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class AddJM_UserRequest : CommandBase<ApiResult<LoginResponse>>
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
