using BNS.Domain.Responses;
using System.ComponentModel.DataAnnotations;
namespace BNS.Domain.Commands
{
    public class LoginRequest : CommandBase<ApiResult<LoginResponse>>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
