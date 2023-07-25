
using System;
using System.Collections.Generic;

namespace BNS.Domain.Commands
{
    public class CreateUsersRequest : CommandBase<ApiResult<object>>
    {
        public List<CreateUsersRequestItem> Users { get; set; }
    }
    public class CreateUsersRequestItem
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid? TeamId { get; set; }
    }
}
