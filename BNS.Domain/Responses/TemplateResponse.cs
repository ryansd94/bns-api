using BNS.Data.Entities.JM_Entities;
using System.Collections.Generic;

namespace BNS.Domain.Responses
{
    public class TemplateResponse
    {
        public List<TemplateResponseItem> Items { get; set; }
    }
    public class TemplateResponseItem : BaseResponseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IssueType { get; set; }
        public string Content { get; set; }
        public virtual ICollection<JM_Status> Status { get; set; }
    }
}
