using System;
using System.ComponentModel.DataAnnotations;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Commands
{
    public class UpdateStatusJM_UserRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public EUserStatus Status { get; set; }
    }
}
