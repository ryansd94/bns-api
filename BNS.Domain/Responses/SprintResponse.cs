using System;
using System.Collections.Generic;

namespace BNS.Domain.Responses
{
    public class SprintResponse
    {
        public List<SprintResponseItem> Items { get; set; }
    }

    public class SprintResponseItem : BaseResponseModel
    {
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? ParentId { get; set; }
        public bool Active { get; set; }
        public List<SprintResponseItem> Childs { get; set; }
    }
}
