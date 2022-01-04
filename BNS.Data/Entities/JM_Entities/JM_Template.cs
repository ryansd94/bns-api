using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Data.Entities.JM_Entities
{
    public class JM_Template : BaseJMEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IssueType { get; set; }
        public string ReporterIssueStatus { get; set; }
        public string AssigneeIssueStatus { get; set; }
    }
}
