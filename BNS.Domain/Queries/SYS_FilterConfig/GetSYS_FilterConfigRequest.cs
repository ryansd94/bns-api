using BNS.Domain.Responses;
namespace BNS.Domain.Queries
{
    public class GetSYS_FilterConfigRequest : CommandRequest<ApiResult<SYS_FilterConfigResponse>>
    {
        public string Component { get; set; }
    }
}

