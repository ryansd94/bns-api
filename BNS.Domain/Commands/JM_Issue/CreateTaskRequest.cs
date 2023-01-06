using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class CreateTaskRequest : CommandBase<ApiResult<Guid>>
    {
        public Dictionary<string, string> DynamicData { get; set; }
        public TaskDefaultData DefaultData { get; set; }
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
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public List<Guid> UsersAssign { get; set; }
        public List<TagItem> Tags { get; set; }
        public decimal? Estimatedhour { get; set; }
    }

    public class TagItem
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public bool IsAddNew { get; set; }
        public bool IsDelete { get; set; }
    }
}
