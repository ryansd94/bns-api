using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Data.Entities
{
  public  class JM_Account : IdentityUser<Guid>
    {
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public Guid? CompanyId { get; set; }
        public bool? IsMainAccount { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUser { get; set; }
        public bool IsDelete { get; set; }
        public string GoogleId { get; set; }
        public string FullName { get; set; }
    }
}
