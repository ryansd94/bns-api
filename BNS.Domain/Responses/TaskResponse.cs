using System;
using System.Collections.Generic;

namespace BNS.Domain.Responses
{
    public class TaskResponse
    {
        public List<TaskItem> Items { get; set; }
    }
    public class TaskItem : BaseResponseModel
    {
        public TaskItem()
        {
            Status = new StatusResponseItem();
        }
        public string Title { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public Guid TaskTypeId { get; set; }
        public Guid? AssignUserId { get; set; }
        public Guid ReporterId { get; set; }
        public Guid? SprintId { get; set; }
        public string Icon { get; set; }
        public string OriginalTime { get; set; }
        public string RemainingTime { get; set; }
        public string TaskTypeName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? TaskParentId { get; set; }
        public Guid StatusId { get; set; }
        public StatusResponseItem Status { get; set; }
        public TaskUser CreateUser { get; set; }
    }

    public class TaskUser
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
