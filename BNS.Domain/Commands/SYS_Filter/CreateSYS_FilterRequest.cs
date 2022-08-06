using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BNS.Domain.Commands
{
    public class CreateSYS_FilterRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
        public string FilterData { get; set; }
        public string View { get; set; }
    }
}
