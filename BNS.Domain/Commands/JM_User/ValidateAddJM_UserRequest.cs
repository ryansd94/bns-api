using BNS.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class ValidateAddJM_UserRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Token { get; set; }
    }
}
