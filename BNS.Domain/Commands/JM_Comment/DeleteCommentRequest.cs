using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Domain.Commands
{
    public class DeleteCommentRequest : CommandBase<ApiResult<Guid>>
    {
        public DeleteCommentRequest()
        {
            ids=new List<Guid>();
        }
        [Required]
        public List<Guid> ids { get; set; }
    }
}
