using BNS.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class RegisterGoogleRequest : CommandBase<ApiResult<LoginResponse>>
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [PasswordValidationAttribute(true)]
        public string Password { get; set; }
    }
}
