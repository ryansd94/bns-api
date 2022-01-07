using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BNS.Data.Entities.JM_Entities
{
    public partial class JM_Sprint : BaseJMEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsComplete { get; set; }
        public Guid JM_ProjectId { get; set; }

        [ForeignKey("JM_ProjectId")]
        public JM_Project JM_Project { get; set; }

        public virtual IEnumerable<JM_Issue> JM_Issues { get; set; }
    }
}
