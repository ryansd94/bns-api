using System;

namespace BNS.Domain.Commands
{
    public class UpdateCommentRequest: CommandUpdateBase<ApiResult<Guid>>
    {
        public string Value { get; set; }
    }
}
