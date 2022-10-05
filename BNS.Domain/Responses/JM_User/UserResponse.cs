using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Responses
{
    public class UserResponse
    {
        public List<UserResponseItem> Items { get; set; }
    }

    public class UserResponseItem : IdentityUser<Guid>
    {
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public bool? IsMainAccount { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUser { get; set; }
        public bool IsDelete { get; set; }
        public string GoogleId { get; set; }
        public string FullName { get; set; }
        public string TeamName { get; set; }
        public string Image { get; set; }
        public EUserStatus Status { get; set; }
        public Guid UserId { get; set; }
        public Guid? TeamId { get; set; }

    }
}
