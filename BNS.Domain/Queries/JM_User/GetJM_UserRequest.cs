using BNS.Domain.Responses;

namespace BNS.Domain.Queries
{
    public class GetJM_UserRequest : CommandRequest<ApiResult<JM_UserResponse>>
    {
        public string keyword { get; set; }
    }
}
