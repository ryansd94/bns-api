using System.Collections.Generic;

namespace BNS.Domain.Responses
{
    public class JM_TemplateResponse
    {
        public List<JM_TemplateResponseItem> Items { get; set; }
    }
    public class JM_TemplateResponseItem : BaseResultModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string IssueType { get; set; }
        public string ReporterIssueStatus { get; set; }
        public string AssigneeIssueStatus { get; set; }
    }
}
