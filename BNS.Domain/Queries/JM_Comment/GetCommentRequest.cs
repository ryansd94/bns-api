using BNS.Domain.Responses;
using System;

namespace BNS.Domain.Queries
{
    public class GetCommentRequest: CommandGetRequest<ApiResult<CommentResponse>>
    {
        public Guid ParentId { get; set; }
    }
}
