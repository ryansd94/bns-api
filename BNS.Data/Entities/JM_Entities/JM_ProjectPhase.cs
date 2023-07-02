using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static BNS.Utilities.Enums;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_ProjectPhase : BaseJMEntity
    {
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? ParentId { get; set; }
        public Guid ProjectId { get; set; }
        public bool Active { get; set; }
        [ForeignKey("ProjectId")]
        public virtual JM_Project JM_Project { get; set; }
        [ForeignKey("ParentId")]
        public virtual JM_ProjectPhase PhaseParent { get; set; }
        public virtual IEnumerable<JM_ProjectPhase> Childs { get; set; }
    }
}
