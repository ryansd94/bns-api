using BNS.Domain.Responses;
namespace BNS.Domain.Queries
{
    public class GetSYS_FilterConfigRequest : CommandGetRequest<ApiResult<FilterConfigResponse>>
    {
        public string Component { get; set; }
    }
}

