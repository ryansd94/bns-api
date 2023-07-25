using BNS.Domain.Responses;
namespace BNS.Domain.Queries
{
    public class GetTeamRequest : CommandGetRequest<ApiResultList<TeamResponseItem>>
    {
        public bool IsParentChild { get; set; } = true;
    }
}
