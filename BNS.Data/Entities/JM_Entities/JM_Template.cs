using System.Collections.Generic;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_Template : BaseJMEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IssueType { get; set; }
        public string Content { get; set; }
        public virtual ICollection<JM_TemplateStatus> TemplateStatus { get; set; }
        public virtual ICollection<JM_TemplateDetail> TemplateDetails { get; set; }
    }
}
