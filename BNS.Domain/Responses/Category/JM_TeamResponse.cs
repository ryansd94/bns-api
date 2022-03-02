using BNS.Data.Entities.JM_Entities;
using System.Collections.Generic;

namespace BNS.Domain.Responses
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
