using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.ViewModels.Responses.Project
{
    public class JM_ProjectResponse
    {
        public List<JM_ProjectResponseItem> Items { get; set; }
    }
    public class JM_ProjectResponseItem : BaseResultModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
