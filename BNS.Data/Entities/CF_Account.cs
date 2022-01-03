using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_Account : IdentityUser<Guid>
    {
        public Guid? SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public Guid ShopIndex { get; set; }
        public bool? IsMainAccount { get; set; }
        public string ShopCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUser { get; set; }
        public bool? IsDelete { get; set; }


        public string BranchDefault { get; set; }
        public CF_Employee Cf_Employee { get; set; }
        public CF_Shop CF_Shop { get; set; }
        public ICollection<JM_Team> JM_TeamsCreate { get; set; }
        public ICollection<JM_Team> JM_TeamsUpdate { get; set; }

        //public List<CF_Bill> CF_Bills { get; set; }
        //public List<CF_Order> CF_Orders { get; set; }

    }
}
