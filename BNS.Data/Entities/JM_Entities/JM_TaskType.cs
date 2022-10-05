using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_TaskType : BaseJMEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public int? Order { get; set; }
        public Guid? TemplateId { get; set; }
        public string Color { get; set; }
        public virtual ICollection<JM_Task> Tasks { get; set; }
        [ForeignKey("TemplateId")]
        public virtual JM_Template Template { get; set; }
    }
}
