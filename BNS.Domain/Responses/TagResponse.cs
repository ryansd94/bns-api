using System.Collections.Generic;

namespace BNS.Domain.Responses
{
    public class TagResponse
    {
        public List<TagResponseItem> Items { get; set; }
    }

    public class TagResponseItem : BaseResponseModel
    {
        public string Name { get; set; }
    }
}
