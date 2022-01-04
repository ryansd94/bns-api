using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Data.Entities.JM_Entities
{
    public partial class JM_ProjectSprint : BaseJMEntity
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
