using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Data.Entities
{
   public class Sys_RoleGroup : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int? Number { get; set; }
        public CF_Shop CF_Shop { get; set; }
    }
}
