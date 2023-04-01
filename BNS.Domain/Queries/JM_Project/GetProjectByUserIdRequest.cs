using BNS.Domain.Responses;
namespace BNS.Domain.Queries
{
    public class GetProjectByUserIdRequest : CommandGetRequest<ApiResultList<ProjectResponseItem>>
    {
    }
}
