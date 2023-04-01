using BNS.Domain.Responses;

namespace BNS.Domain.Queries
{
    public class GetCheckStatusRequest: CommandGetRequest<ApiResult<StatusCheckResponse>>
    {
    }
}
