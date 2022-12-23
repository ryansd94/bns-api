using BNS.Domain.Responses;

namespace BNS.Domain.Queries
{
    public class GetUserRequest : CommandGetRequest<ApiResult<UserResponse>>
    {
        public string keyword { get; set; }
    }
}
