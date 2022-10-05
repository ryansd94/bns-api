using BNS.Domain.Responses;
namespace BNS.Domain.Queries
{
    public class GetTaskTypeByIdRequest : CommandByIdRequest<ApiResult<TaskTypeItem>>
    {
    }
}
