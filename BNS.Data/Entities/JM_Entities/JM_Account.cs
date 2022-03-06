using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static BNS.Utilities.Enums;

namespace BNS.Data.Entities.JM_Entities
{
  public  class JM_Account : IdentityUser<Guid>
    {
        [MaxLength(200)]
        public string FullName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUser { get; set; }
        public bool IsDelete { get; set; }
        public string GoogleId { get; set; }
        public virtual ICollection<JM_Team> JM_TeamsCreate { get; set; }
        public virtual ICollection<JM_Team> JM_TeamsUpdate { get; set; }
        public virtual ICollection<JM_Issue> JM_IssueAssign { get; set; }
        public virtual ICollection<JM_Issue> JM_IssueReport { get; set; }
        public virtual ICollection<JM_AccountCompany> JM_AccountCompanys { get; set; }
        public virtual ICollection<JM_TeamMember> JM_TeamMembers { get; set; }

    }
}
