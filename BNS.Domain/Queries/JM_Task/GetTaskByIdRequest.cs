using BNS.Domain.Responses;
namespace BNS.Domain.Queries
{
    public class GetTaskByIdRequest : CommandByIdRequest<ApiResult<TaskByIdResponse>>
    {
    }
}
