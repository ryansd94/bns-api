using BNS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BNS.Domain.Commands
{
    public class SendMailAddJM_UserRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public List<string> Emails { get; set; }
    }
}
