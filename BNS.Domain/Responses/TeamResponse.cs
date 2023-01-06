using System;
using System.Collections.Generic;

namespace BNS.Domain.Responses
{
    public class TeamResponse
    {
        public List<TeamResponseItem> Items { get; set; }
    }

    public class TeamResponseItem  : BaseResponseModel
    { 
        public string ParentName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public List<Guid> TeamMembers { get; set; }
    }
}
