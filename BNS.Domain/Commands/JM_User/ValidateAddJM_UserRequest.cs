using BNS.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class ValidateAddJM_UserRequest : CommandBase<ApiResult<ValidateAddJM_UserResponse>>
    {
        [Required]
        public string Token { get; set; }
    }
}
