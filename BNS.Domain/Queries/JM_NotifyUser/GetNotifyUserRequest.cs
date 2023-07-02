using BNS.Domain.Responses;

namespace BNS.Domain.Queries
{
    public class GetNotifyUserRequest : CommandGetRequest<ApiResult<NotifyUserResponse>>
    {
        public bool? IsRead { get; set; }
    }
}
