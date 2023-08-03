using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Commands
{
    public class CreateTaskRequest : CommandBase<ApiResult<Guid>>
    {
        public Dictionary<string, string> DynamicData { get; set; }
        public TaskDefaultData DefaultData { get; set; }
        public List<TaskCommentRequest> Comments { get; set; }
    }

    public class TaskDefaultData
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public Guid TaskTypeId { get; set; }
        public Guid StatusId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? PriorityId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public List<Guid> UsersAssignId { get; set; }
        public List<TagItem> Tags { get; set; }
        public decimal? Estimatedhour { get; set; }
        public List<Guid> TaskChildDelete { get; set; }
        public List<Guid> TaskChild { get; set; }
        public List<FileUpload> Files { get; set; }
    }

    public class TaskParent
    {
        public Guid? Id { get; set; }
    }

    public class TagItem
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public bool IsAddNew { get; set; }
        public bool IsDelete { get; set; }
        public ERowStatus? RowStatus { get; set; }
    }

    public class TaskCommentRequest
    {
        public string Value { get; set; }
        public Guid? Id { get; set; }
        public bool IsAddNew { get; set; }
        public List<TaskCommentRequest> Childrens { get; set; }
    }
}
