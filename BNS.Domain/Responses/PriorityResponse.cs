using System.Collections.Generic;

namespace BNS.Domain.Responses
{
    public class PriorityResponse
    {
        public List<PriorityResponseItem> Items { get; set; }
    }

    public class PriorityResponseItem : BaseResponseModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int Order { get; set; }
    }
}
