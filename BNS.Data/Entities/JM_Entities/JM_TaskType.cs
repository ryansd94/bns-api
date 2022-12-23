using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_TaskType : BaseJMEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string ColorFilter { get; set; }
        public int? Order { get; set; }
        public Guid? TemplateId { get; set; }
        public string Color { get; set; }
        [ForeignKey("TemplateId")]
        public virtual JM_Template Template { get; set; }
    }
}
