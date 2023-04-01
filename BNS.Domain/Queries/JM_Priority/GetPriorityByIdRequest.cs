
using BNS.Domain.Responses;

namespace BNS.Domain.Queries
{
    public class GetPriorityByIdRequest : CommandByIdRequest<ApiResult<PriorityResponseItem>>
    {
    }
}
