using BNS.Domain.Responses;
namespace BNS.Domain.Queries
{
    public class GetTaskTypeByIdRequest : CommandByIdRequest<ApiResult<TaskTypeItem>>
    {
        public bool? IsDelete { get; set; } = false;
    }
}
