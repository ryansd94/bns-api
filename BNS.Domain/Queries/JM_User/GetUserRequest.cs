using BNS.Domain.Responses;

namespace BNS.Domain.Queries
{
    public class GetUserRequest : CommandRequest<ApiResult<UserResponse>>
    {
        public string keyword { get; set; }
    }
}
