using BNS.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class RegisterGoogleRequest : CommandBase<ApiResult<LoginResponse>>
    {
        [Required]
        public string Token { get; set; }
    }
}
