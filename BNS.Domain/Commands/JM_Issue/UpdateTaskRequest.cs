using System;
using System.Collections.Generic;
namespace BNS.Domain.Commands
{
    public class UpdateTaskRequest : CommandUpdateBase<ApiResult<Guid>>
    {
        public Dictionary<Guid, string> DynamicData { get; set; }
        public TaskDefaultData DefaultData { get; set; }
    }
}
