using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfPriceWithArea
    {
        public long Index { get; set; }
        public long? AreaIndex { get; set; }
        public long? RoomIndex { get; set; }
        public long? ProductIndex { get; set; }
        public decimal? Price { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? ShopIndex { get; set; }
        public int? IsDelete { get; set; }
        public int? BranchIndex { get; set; }
    }
}
