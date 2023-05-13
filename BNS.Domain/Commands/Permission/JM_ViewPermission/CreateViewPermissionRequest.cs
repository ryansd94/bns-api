using System;
using System.Collections.Generic;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Commands
{
    public class CreateViewPermissionRequest : CommandBase<ApiResult<Guid>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ViewPermissionAction> Permission { get; set; }
        public List<Guid> UserSelectedIds { get; set; }
        public List<Guid> TeamSelectedIds { get; set; }
    }

    public class ViewPermissionAction
    {
        public string View { get; set; }
        public List<ViewActionItem> Actions { get; set; }
    }

    public class ViewActionItem
    {
        public string Key { get; set; }
        public bool Value { get; set; }
    }
}
