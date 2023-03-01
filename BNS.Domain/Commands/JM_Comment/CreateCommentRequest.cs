using System;

namespace BNS.Domain.Commands
{
    public class CreateCommentRequest: CommandBase<ApiResult<Guid>>
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public Guid? ParentId { get; set; }
        public string Value { get; set; }
    }
}
