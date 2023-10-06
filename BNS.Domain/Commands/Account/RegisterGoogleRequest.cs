using BNS.Domain.Responses;
using System.ComponentModel.DataAnnotations;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Commands
{
    public class RegisterGoogleRequest : CommandBase<ApiResult<LoginResponse>>
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [PasswordValidationAttribute(true)]
        public string Password { get; set; }
        [Required]
        public string Organization { get; set; }
        [Required]
        public string GoogleToken { get; set; }
        [Required]
        public EUserType UserType { get; set; }
        public EScale Scale { get; set; }
    }
}
