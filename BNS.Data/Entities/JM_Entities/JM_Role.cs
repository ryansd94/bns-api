using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Data.Entities.JM_Entities
{
   public class JM_Role : IdentityRole<Guid>
    {
        public string Permission { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUser { get; set; }
        public Guid? CompanyId { get; set; }
        public bool  IsDelete { get; set; }
    }
}
