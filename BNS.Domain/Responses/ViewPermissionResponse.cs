using BNS.Domain.Commands;
using System;
using System.Collections.Generic;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Responses
{
    public class ViewPermissionResponse
    {
        public List<ViewPermissionResponseItem> Items { get; set; }
    }

    public class ViewPermissionResponseItem : BaseResponseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ViewPermissionByIdResponse : ViewPermissionResponseItem
    {
        public List<ViewPermissionAction> Permission { get; set; }
        public List<ViewPermissionObjectResponse> Objects { get; set; }
    }

    public class ViewPermissionObjectResponse
    {
        public Guid Id { get; set; }
        public EPermissionObject ObjectType { get; set; }
    }
}
