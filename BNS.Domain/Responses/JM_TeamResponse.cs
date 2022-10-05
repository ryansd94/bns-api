using BNS.Data.Entities.JM_Entities;
using System;
using System.Collections.Generic;

namespace BNS.Domain.Responses
{
    public class JM_TeamResponse
    {
        public List<JM_TeamResponseItem> Items { get; set; }
    }

    public class JM_TeamResponseItem  : BaseResponseModel
    { 
        public string ParentName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public List<Guid> TeamMembers { get; set; }
    }
}
