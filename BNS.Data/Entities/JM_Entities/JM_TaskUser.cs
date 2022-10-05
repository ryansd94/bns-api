using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_TaskUser : BaseJMEntity
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("TaskId")]
        public virtual JM_Task Task { get; set; }
        [ForeignKey("UserId")]
        public virtual JM_Account User { get; set; }
    }
}
