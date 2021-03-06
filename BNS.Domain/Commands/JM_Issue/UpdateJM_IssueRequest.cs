using System;
using System.ComponentModel.DataAnnotations;
using static BNS.Utilities.Enums;
namespace BNS.Domain.Commands
{
    public class UpdateJM_IssueRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Summary { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid Id { get; set; }
        [Required]
        public EIssueType IssueType { get; set; }
        public EIssueStatus IssueStatus { get; set; }
        public Guid? AssignUserId { get; set; }
        public Guid? SprintId { get; set; }
        public string OriginalTime { get; set; }
        public string RemainingTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? IssueParentId { get; set; }
    }
}
