using BNS.Models;
using BNS.Models.Responses.Project;

namespace BNS.Domain.Queries 
{
    public class GetJM_ProjectByUserIdRequest : CommandByIdRequest<ApiResult<JM_ProjectResponseItem>>
    {
    }
}
