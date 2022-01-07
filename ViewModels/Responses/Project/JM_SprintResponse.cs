using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.ViewModels.Responses.Project
{ 
    public class JM_SprintResponse
    {
        public List<JM_SprintResponseItem> Items { get; set; }
    }

    public class JM_SprintResponseItem : BaseResultModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsComplete { get; set; }
    }
}
