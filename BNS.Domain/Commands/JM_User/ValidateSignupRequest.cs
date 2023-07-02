﻿using BNS.Domain.Responses;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class ValidateSignupRequest : CommandBase<ApiResult<ValidateUserResponse>>
    {
        [Required]
        public string Token { get; set; }
    }
}
