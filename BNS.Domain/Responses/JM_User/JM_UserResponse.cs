using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Responses
{
    public class JM_UserResponse
    {
        public List<JM_UserResponseItem> Items { get; set; }
    }

    public class JM_UserResponseItem : IdentityUser<Guid>
    {
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public bool? IsMainAccount { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUser { get; set; }
        public bool IsDelete { get; set; }
        public string GoogleId { get; set; }
        public string FullName { get; set; }
        public EUserStatus? Status { get; set; }
    }
}
