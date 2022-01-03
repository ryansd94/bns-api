using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Data.Entities
{
    public class Sys_Role : IdentityRole<Guid>
    {
        public string Description { get; set; }
        public string Permission { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUser { get; set; }
        public Guid ShopIndex { get; set; }
        public bool? IsDelete { get; set; }
    }
}
