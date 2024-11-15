﻿
using BNS.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class SignupRequest : CommandBase<ApiResult<LoginResponse>>
    {
        [Required]
        public string Token { get; set; }
        public string FullName { get; set; }
        [PasswordValidationAttribute(false)]
        public string Password { get; set; }
        public bool IsHasAccount { get; set; }
        public string Image { get; set; }
    }
}
