using System;
using System.Collections.Generic;
using static BNS.Utilities.Enums;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_Project : BaseJMEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Avartar { get; set; }
        public string Icon { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EProjectType Type { get; set; }
        public virtual IEnumerable<JM_ProjectTeam> JM_ProjectTeams { get; set; }
        public virtual IEnumerable<JM_ProjectMember> JM_ProjectMembers { get; set; }
        public virtual IEnumerable<JM_Task> JM_Issues { get; set; }
        public virtual IEnumerable<JM_ProjectPhase> Sprints { get; set; }
    }
}
