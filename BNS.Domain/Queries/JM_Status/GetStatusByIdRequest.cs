
using BNS.Domain.Responses;

namespace BNS.Domain.Queries
{
    public class GetStatusByIdRequest : CommandByIdRequest<ApiResult<StatusResponseItem>>
    {
    }
}
