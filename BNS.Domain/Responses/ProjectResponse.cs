using System;
using System.Collections.Generic;

namespace BNS.Domain.Responses
{
    public class ProjectResponse
    {
        public List<ProjectResponseItem> Items { get; set; }
    }
    public class ProjectResponseItem : BaseResponseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
