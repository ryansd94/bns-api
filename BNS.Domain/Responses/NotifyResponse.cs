using System;
using System.Collections.Generic;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Responses
{
    public class NotifyResponse
    {
        public Guid Id { get; set; }
        public Guid? ObjectId { get; set; }
        public ENotifyObjectType Type { get; set; }
        public string Content { get; set; }
        public Guid UserReceivedId { get; set; }
        public bool IsRead { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }

    public class NotifyUserResponse
    {
        public List<NotifyResponse> Items { get; set; }
        public int Unread { get; set; }
    }

    public class NotifyTaskMention
    {
        public User UserMention { get; set; }
        public TaskNotify TaskContent { get; set; }
    }

    public class TaskNotify
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public TaskType TaskType { get; set; }
    }
}
