
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BNS.Domain.Commands
{
    public class SendMailAddUserRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public List<CreateUsersRequestItem> Users { get; set; }
    }
}
