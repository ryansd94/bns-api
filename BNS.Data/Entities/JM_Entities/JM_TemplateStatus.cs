using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_TemplateStatus : BaseJMEntity
    {
        public Guid StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual JM_Status JM_Status { get; set; }
        public Guid TemplateId { get; set; }
        [ForeignKey("TemplateId")]
        public virtual JM_Template JM_Template { get; set; }
    }
}
