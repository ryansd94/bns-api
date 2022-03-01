using System;
using System.Collections.Generic;
using System.Text;
using static BNS.Utilities.Enums;

namespace BNS.Models.Responses.Project
{
   public class JM_IssueResponse
    {
        public List<JM_IssueResponseItem> Items { get; set; }
    }
    public class JM_IssueResponseItem : BaseResultModel
    {
        public string Summary { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public EIssueType IssueType { get; set; }
        public Guid? AssignUserId { get; set; }
        public Guid ReporterId { get; set; }
        public Guid? SprintId { get; set; }
        public string OriginalTime { get; set; }
        public string RemainingTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? IssueParentId { get; set; }
        public EIssueStatus IssueStatus { get; set; }
    }
}
