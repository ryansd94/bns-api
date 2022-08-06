using System;
using System.Collections.Generic;
using System.Text;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Responses
{
   public class JM_TaskResponse
    {
        public List<JM_TaskResponseItem> Items { get; set; }
    }
    public class JM_TaskResponseItem : BaseResultModel
    {
        public string Summary { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public Guid TaskTypeId { get; set; }
        public Guid? AssignUserId { get; set; }
        public Guid ReporterId { get; set; }
        public Guid? SprintId { get; set; }
        public string OriginalTime { get; set; }
        public string RemainingTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? TaskParentId { get; set; }
        public Guid StatusId { get; set; }
    }
}
