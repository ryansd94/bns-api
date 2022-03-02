using System;
using System.Collections.Generic;

namespace BNS.Domain.Commands 
{
    public class UpdateMemberJM_TeamRequest : CommandBase<ApiResult<Guid>>
    {
        public Guid Id { get; set; }
        public List<Guid> Members { get; set; }
    }
}
