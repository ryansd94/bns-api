using System.Collections.Generic;

namespace BNS.Domain.Responses
{
    public class StatusResponse
    {
        public List<StatusResponseItem> Items { get; set; }
    }

    public class StatusResponseItem : BaseResponseModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsStatusStart { get; set; }
        public bool IsStatusEnd { get; set; }
        public bool IsAutomaticAdd { get; set; }
        public bool IsApplyAll { get; set; }
        public bool IsActive { get; set; }
    }

    public class StatusCheckResponse
    {
        public bool IsStatusStart { get; set; }
        public bool IsStatusEnd { get; set; }
    }
}
