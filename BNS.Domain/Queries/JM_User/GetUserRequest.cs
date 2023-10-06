using BNS.Domain.Responses;
using System;
using System.Collections.Generic;

namespace BNS.Domain.Queries
{
    public class GetUserRequest : CommandGetRequest<ApiResult<UserResponse>>
    {
        public string keyword { get; set; }
        public bool isHasNotTeam { get; set; } = false;
    }
}
