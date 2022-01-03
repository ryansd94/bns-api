using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_PriceWithArea
    {
        public Guid Index { get; set; }
        public Guid? AreaIndex { get; set; }
        public Guid? RoomIndex { get; set; }
        public Guid? ProductIndex { get; set; }
        public decimal? Price { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public Guid? ShopIndex { get; set; }
        public int? IsDelete { get; set; }
        public Guid? BranchIndex { get; set; }
    }
}
