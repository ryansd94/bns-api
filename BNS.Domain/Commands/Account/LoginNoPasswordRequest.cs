using BNS.Domain.Responses;
using System.ComponentModel.DataAnnotations;
namespace BNS.Domain.Commands
{
    public class LoginNoPasswordRequest : CommandBase<ApiResult<LoginResponse>>
    {
        [Required]
        public string Username { get; set; }
    }
}
