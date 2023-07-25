using BNS.Domain.Responses;
namespace BNS.Domain.Queries
{
    public class GetTeamByIdRequest : CommandByIdRequest<ApiResult<TeamResponseItemById>>
    {
    }
}
