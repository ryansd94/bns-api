using BNS.Domain.Responses;
using System;

namespace BNS.Domain.Queries
{
    public class GetUserAssignRequest : CommandGetRequest<ApiResult<UserResponse>>
    {
        public Guid? ProjectId { get; set; }
    }
}
