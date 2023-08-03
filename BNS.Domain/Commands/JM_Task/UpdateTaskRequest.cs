using System;
using System.Collections.Generic;

namespace BNS.Domain.Commands
{
    public class UpdateTaskRequest : CommandUpdateBase<ApiResult<Guid>>
    {
        public UpdateTaskChangeFieldItem ChangeFields { get; set; }
    }

    public class UpdateTaskChangeFieldItem
    {
         public List<ChangeFieldItem> DefaultData { get; set; }
         public List<ChangeFieldItem> DynamicData { get; set; }
    }
}
