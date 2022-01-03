using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_ShopIpsetting
    {
        public Guid Index { get; set; }
        public Guid? ShopIndex { get; set; }
        public Guid? BranchIndex { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public string IpAddress { get; set; }
        public string Description { get; set; }
    }
}
