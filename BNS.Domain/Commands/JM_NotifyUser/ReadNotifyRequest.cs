using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class ReadNotifyRequest : CommandUpdateBase<ApiResult<Guid>>
    {
        [Required]
        public Guid UserRecivedId { get; set; }
        public bool IsRead { get; set; }
    }
}
