using System;
using System.Collections.Generic;
using System.Text;
using static BNS.Utilities.Enums;

namespace BNS.Data.Entities
{
   public class Sys_RoleClaim : BaseEntity
    {
        public string Roles { get; set; }
        public Guid DataId { get; set; }
        public ERoleType Type { get; set; }
    }
}
