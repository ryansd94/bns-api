using System;
using System.ComponentModel.DataAnnotations.Schema;
using static BNS.Utilities.Enums;

namespace BNS.Data.Entities.JM_Entities
{
    public partial class JM_Issue : BaseJMEntity
    {
        public string Summary { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public Guid ProjectId { get; set; }
        public EIssueType IssueType { get; set; }
        public Guid? AssignUserId { get; set; }
        public Guid  ReporterId { get; set; }
        public Guid? SprintId { get; set; }
        public string OriginalTime { get; set; }
        public string RemainingTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? IssueParentId { get; set; }
        public EIssueStatus IssueStatus{ get; set; }

        [ForeignKey("ProjectId")]
        public virtual JM_Project JM_Project { get; set; }
        [ForeignKey("SprintId")]
        public virtual JM_Sprint JM_Sprint { get; set; }
        [ForeignKey("IssueParentId")]
        public virtual JM_Issue JM_IssueParent { get; set; }
        [ForeignKey("AssignUserId")]
        public virtual JM_Account JM_AccountAssign { get; set; }
        [ForeignKey("ReporterId")]
        public virtual JM_Account JM_AccountReporter { get; set; }

        
    }
}
