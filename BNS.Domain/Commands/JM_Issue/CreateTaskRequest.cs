using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Commands
{
    public class CreateTaskRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Title { get; set; }
        public dynamic Data { get; set; }
        public Guid TaskTypeId { get; set; }
        public Guid StatusId { get; set; }
        public List<Guid> UsersAssign { get; set; }
    }
}
