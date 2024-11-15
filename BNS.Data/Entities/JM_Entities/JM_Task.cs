﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace BNS.Data.Entities.JM_Entities
{
    [Index(nameof(ProjectId), Name = "Nidx_JM_Task_ProjectId")]
    [Index(nameof(SprintId), Name = "Nidx_JM_Task_SprintId")]
    [Index(nameof(ParentId), Name = "Nidx_JM_Task_ParentId")]
    [Index(nameof(StatusId), Name = "Nidx_JM_Task_StatusId")]
    [Index(nameof(PriorityId), Name = "Nidx_JM_Task_PriorityId")]
    [Index(nameof(AssignUserId), Name = "Nidx_JM_Task_AssignUserId")]
    [Index(nameof(TaskTypeId), Name = "Nidx_JM_Task_TaskTypeId")]
    public class JM_Task : BaseJMEntity
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid TaskTypeId { get; set; }
        public Guid? AssignUserId { get; set; }
        public Guid ReporterId { get; set; }
        public Guid? SprintId { get; set; }
        //public string OriginalTime { get; set; }
        public string RemainingTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? ParentId { get; set; }
        public Guid StatusId { get; set; }
        public Guid? PriorityId { get; set; }
        public decimal? Estimatedhour { get; set; }

        [ForeignKey("ProjectId")]
        public virtual JM_Project JM_Project { get; set; }
        [ForeignKey("SprintId")]
        public virtual JM_ProjectPhase JM_ProjectPhase { get; set; }
        [ForeignKey("ParentId")]
        public virtual JM_Task JM_TaskParent { get; set; }
        //[ForeignKey("AssignUserId")]
        //public virtual JM_Account JM_AccountAssign { get; set; }
        //[ForeignKey("ReporterId")]
        //public virtual JM_Account JM_AccountReporter { get; set; }
        [ForeignKey("StatusId")]
        public virtual JM_Status Status { get; set; }
        [ForeignKey("PriorityId")]
        public virtual JM_Priority Priority { get; set; }
        [ForeignKey("AssignUserId")]
        public virtual JM_AccountCompany AssignUser { get; set; }
        [ForeignKey("TaskTypeId")]
        public virtual JM_TaskType TaskType { get; set; }
        public virtual ICollection<JM_TaskCustomColumnValue> TaskCustomColumnValues { get; set; }
        public virtual ICollection<JM_TaskTag> TaskTags { get; set; }
        public virtual ICollection<JM_CommentTask> CommentTasks { get; set; }
        public virtual ICollection<JM_Task> Childs { get; set; }
    }
}
