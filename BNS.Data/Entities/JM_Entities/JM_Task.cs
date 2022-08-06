using System;
using System.ComponentModel.DataAnnotations.Schema;
using static BNS.Utilities.Enums;

namespace BNS.Data.Entities.JM_Entities
{
    public partial class JM_Task : BaseJMEntity
    {
        public string Summary { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TaskTypeId { get; set; }
        public Guid? AssignUserId { get; set; }
        public Guid  ReporterId { get; set; }
        public Guid? SprintId { get; set; }
        public string OriginalTime { get; set; }
        public string RemainingTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? TaskParentId { get; set; }
        public Guid StatusId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual JM_Project JM_Project { get; set; }
        [ForeignKey("SprintId")]
        public virtual JM_Sprint JM_Sprint { get; set; }
        [ForeignKey("IssueParentId")]
        public virtual JM_Task JM_TaskParent { get; set; }
        [ForeignKey("AssignUserId")]
        public virtual JM_Account JM_AccountAssign { get; set; }
        [ForeignKey("ReporterId")]
        public virtual JM_Account JM_AccountReporter { get; set; }
        [ForeignKey("StatusId")]
        public virtual JM_Status JM_Status { get; set; }


    }
}
