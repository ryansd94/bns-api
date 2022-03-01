using BNS.Models;
using BNS.Models.Responses.Project;

namespace BNS.Domain.Queries
{
    public class GetJM_TemplateByIdRequest : CommandByIdRequest<ApiResult<JM_TemplateResponseItem>>
    {
    }
}
