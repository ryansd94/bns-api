using System;
using System.ComponentModel.DataAnnotations;
using static BNS.Utilities.Enums;
namespace BNS.Domain.Commands
{
    public class UpdateTaskRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Summary { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid TaskTypeId { get; set; }
        public Guid StatusId { get; set; }
        public Guid? AssignUserId { get; set; }
        public Guid? SprintId { get; set; }
        public string OriginalTime { get; set; }
        public string RemainingTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? TaskParentId { get; set; }
    }
}
