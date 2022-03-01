using BNS.Models;
using BNS.Models.Responses.Project;
namespace BNS.Domain.Queries
{
    public class GetJM_SprintByIdRequest : CommandByIdRequest<ApiResult<JM_SprintResponseItem>>
    {
    }
}
