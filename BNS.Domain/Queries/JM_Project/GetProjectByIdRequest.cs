using BNS.Domain.Responses;

namespace BNS.Domain.Queries
{
    public class GetProjectByIdRequest : CommandByIdRequest<ApiResult<ProjectResponseItem>>
    {
    }
}
