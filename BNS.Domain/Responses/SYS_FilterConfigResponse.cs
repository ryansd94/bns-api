using System.Collections.Generic;
namespace BNS.Domain.Responses
{
    public class SYS_FilterConfigResponse
    {
        public List<SYS_FilterConfigResponseItem> Items { get; set; }
    }

    public class SYS_FilterConfigResponseItem : BaseResponseModel
    {
        public string Name { get; set; }
        public string FilterData { get; set; }
    }
}
