using BNS.Models;
using BNS.Models.Responses;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class LoginGoogleRequest : CommandBase<ApiResult<CF_AccountLoginResponseModel>>
    {
        [Required]
        public string Token { get; set; }
    }
}
