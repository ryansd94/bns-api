using BNS.Domain.Responses;

namespace BNS.Domain.Queries
{
    public class GetTemplateByIdRequest : CommandByIdRequest<ApiResult<TemplateResponseItem>>
    {
    }
}
