using BNS.Domain.Commands;
using BNS.Utilities;
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
        public List<CommentResponseItem> Comments { get; set; }
    }
    public class TaskItem : BaseResponseModel
    {
        public TaskItem()
        {
            Status = new StatusResponseItem();
            TaskType = new TaskType();
        }
        public int Number { get; set; }
        public string Title { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public Guid TaskTypeId { get; set; }
        public Guid AssignUserId { get; set; }
        public List<Guid> UsersAssignId { get; set; }
        public List<User> UsersAssign { get; set; }
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
        public Guid? ProjectId { get; set; }
        public Guid StatusId { get; set; }
        public decimal? Estimatedhour { get; set; }
        public StatusResponseItem Status { get; set; }
        public User CreatedUser { get; set; }
        public ICollection<TaskCustomColumnValue> TaskCustomColumnValues { get; set; }
        public List<TagItem> Tags { get; set; }
        public List<TaskItem> Childs { get; set; }
        public TaskChildItem TaskParent { get; set; }
        public List<FileUpload> Files { get; set; }
    }

    public class TaskChildItem
    {
        public Guid Id { get; set; }
        public TaskType TaskType { get; set; }
        public string Title { get; set; }
        public StatusResponseItem Status { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
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
        public string FullName { get { return Ultility.GetFullName(FirstName, LastName); } }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class TaskCalendarRespone
    {
        public List<TaskCalendarResource> Resources { get; set; }
        public List<TaskCalendarEventItem> Events { get; set; }
    }

    public class TaskCalendarResource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public bool GroupOnly { get; set; }

    }
    public class TaskCalendarEventItem
    {
        public Guid Id { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public Guid? ResourceId { get; set; }
        public string Title { get; set; }
        public User UserAssign { get; set; }
        public TaskType TaskType { get; set; }
        public StatusResponseItem Status { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
