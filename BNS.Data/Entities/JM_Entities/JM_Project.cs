using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Data.Entities.JM_Entities
{
    public partial class JM_Project : BaseJMEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Avartar { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual IEnumerable<JM_ProjectTeam> JM_ProjectTeams { get; set; }
        public virtual IEnumerable<JM_ProjectMember> JM_ProjectMembers { get; set; }
    }
}
