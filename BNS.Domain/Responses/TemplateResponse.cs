using BNS.Domain.Commands;
using System;
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
        public virtual List<StatusItemResponse> Status { get; set; }
    }

    public class StatusItemResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsStatusStart { get; set; }
        public bool IsStatusEnd { get; set; }
    }
}
