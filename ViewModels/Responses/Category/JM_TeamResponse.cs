using BNS.Data.Entities.JM_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Models.Responses
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
