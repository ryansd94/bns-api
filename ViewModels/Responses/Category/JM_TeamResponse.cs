using BNS.Data.Entities.JM_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.ViewModels.Responses.Category
{
    public class JM_TeamResponse
    {
        public List<JM_TeamResponseItem> Items { get; set; }
    }

    public class JM_TeamResponseItem : JM_Team
    { 
        public string ParentName { get; set; }
    }
}
