using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_TaskCustomColumnValue : BaseJMEntity
    {
        public Guid TaskId { get; set; }
        public Guid CustomColumnId { get; set; }
        public Guid TemplateDetailId { get; set; }
        public string Value { get; set; }
        [ForeignKey("TaskId")]
        public virtual JM_Task Task { get; set; }
        [ForeignKey("CustomColumnId")]
        public virtual JM_CustomColumn CustomColumn { get; set; }
        [ForeignKey("TemplateDetailId")]
        public virtual JM_TemplateDetail TemplateDetail { get; set; }
    }
}
