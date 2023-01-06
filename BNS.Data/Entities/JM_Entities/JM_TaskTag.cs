using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_TaskTag : BaseJMEntity
    {
        public Guid TaskId { get; set; }
        public Guid TagId { get; set; }
        public string Value { get; set; }
        [ForeignKey("TaskId")]
        public virtual JM_Task Task { get; set; }
        [ForeignKey("TagId")]
        public virtual JM_Tag Tag { get; set; }
    }
}
