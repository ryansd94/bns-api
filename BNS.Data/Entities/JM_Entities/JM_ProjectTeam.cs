using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BNS.Data.Entities.JM_Entities
{
    public partial class JM_ProjectTeam : BaseJMEntity
    {
        public Guid ProjectId { get; set; }
        public Guid TeamId { get; set; }
        [ForeignKey("TeamId")]
        public virtual JM_Team JM_Team { get; set; }
        [ForeignKey("ProjectId")]
        public virtual JM_Project Project { get; set; }
    }
}
