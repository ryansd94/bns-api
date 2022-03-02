using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class CreateJM_TemplateRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string IssueType { get; set; }
        public string ReporterIssueStatus { get; set; }
        public string AssigneeIssueStatus { get; set; }
    }
}
