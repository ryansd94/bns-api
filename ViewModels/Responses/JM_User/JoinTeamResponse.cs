using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.ViewModels.Responses
{
    public class JoinTeamResponse
    {
        public string Key { get; set; }
        public string EmailJoin { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid  UserRequest { get; set; }
    }
}
