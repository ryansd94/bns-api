using BNS.Data.Entities.JM_Entities;
using System;
using System.Collections.Generic;
namespace BNS.Domain.Responses
{
    public class TaskTypeResponse
    {
        public List<TaskTypeItem> Items { get; set; }
    }
    public class TaskTypeItem : BaseResponseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public string ColorFilter { get; set; }
        public int? Order { get; set; }
        public Guid? TemplateId { get; set; }
        public string TemplateName { get; set; }
        public virtual JM_Template Template { get; set; }
    }
}
