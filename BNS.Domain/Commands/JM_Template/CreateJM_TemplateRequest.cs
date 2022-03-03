using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Commands
{
    public class CreateJM_TemplateRequest : CommandBase<ApiResult<Guid>>
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<EIssueType> IssueTypes { get; set; }
        public List<EIssueStatus> ReporterIssueStatus { get; set; }
        public List<EIssueStatus> AssigneeIssueStatus { get; set; }
    }
}
