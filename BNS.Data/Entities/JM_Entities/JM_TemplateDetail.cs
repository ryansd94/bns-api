using System;
using System.ComponentModel.DataAnnotations.Schema;
using static BNS.Utilities.Enums;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_TemplateDetail : BaseJMEntity
    {
        public int Order { get; set; }
        public EColumnPosition ColumnPosition { get; set; }
        public Guid? CustomColumnId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid TemplateId { get; set; }
        public string ColumnName { get; set; }
        public string ColumnTitle { get; set; }
        [ForeignKey("CustomColumnId")]
        public virtual JM_CustomColumn CustomColumn { get; set; }
        [ForeignKey("TemplateId")]
        public virtual JM_Template Template { get; set; }
    }
}
