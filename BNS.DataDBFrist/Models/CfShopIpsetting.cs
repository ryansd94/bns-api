using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfShopIpsetting
    {
        public long Index { get; set; }
        public long? ShopIndex { get; set; }
        public long? BranchIndex { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public string IpAddress { get; set; }
        public string Description { get; set; }
    }
}
