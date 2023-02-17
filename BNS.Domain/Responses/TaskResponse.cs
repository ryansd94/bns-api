using BNS.Domain.Commands;
using System;
using System.Collections.Generic;

namespace BNS.Domain.Responses
{
    public class TaskResponse
    {
        public List<TaskItem> Items { get; set; }
    }
    public class TaskByIdResponse
    {
        public TaskTypeItem TaskType { get; set; }
        public TaskItem Task { get; set; }
        public List<TaskCommentReponse> Comments { get; set; }
    }
    public class TaskItem : BaseResponseModel
    {
        public TaskItem()
        {
            Status = new StatusResponseItem();
            TaskType = new TaskType();
        }
        public string Title { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public Guid TaskTypeId { get; set; }
        public List<Guid> UsersAssign { get; set; }
        public Guid ReporterId { get; set; }
        public Guid? SprintId { get; set; }
        public TaskType TaskType { get; set; }
        public string Icon { get; set; }
        public string IconColor { get; set; }
        public string OriginalTime { get; set; }
        public string RemainingTime { get; set; }
        public string TaskTypeName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? ParentId { get; set; }
        public Guid StatusId { get; set; }
        public decimal? Estimatedhour { get; set; }
        public StatusResponseItem Status { get; set; }
        public User CreatedUser { get; set; }
        public ICollection<TaskCustomColumnValue> TaskCustomColumnValues { get; set; }
        public List<TagItem> Tags { get; set; }
        public List<TaskItem> TaskChilds { get; set; }
        public TaskItem TaskParent { get; set; }
        public List<FileUpload> Files { get; set; }
    }

    public class TaskCustomColumnValue
    {
        public Guid CustomColumnId { get; set; }
        public string Value { get; set; }
    }

    public class TaskType
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
    }

    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
    }

    public class TaskCommentReponse : TaskCommentRequest
    {
        public User User { get; set; }
        public string UpdatedTime { get; set; }
    }
}
