using System.Collections.Generic;
namespace BNS.Domain.Responses
{
    public class FilterConfigResponse
    {
        public List<FilterConfigResponseItem> Items { get; set; }
    }

    public class FilterConfigResponseItem : BaseResponseModel
    {
        public string Name { get; set; }
        public string FilterData { get; set; }
    }
}
