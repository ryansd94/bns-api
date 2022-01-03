using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_TableOrder
    {
        public Guid Index { get; set; }
        public Guid? RoomIndex { get; set; }
        public Guid? CustomerIndex { get; set; }
        public bool? IsPaid { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public Guid? ShopIndex { get; set; }
        public Guid? BranchIndex { get; set; }
        public bool? IsBook { get; set; }
        public bool? IsPrintBill { get; set; }
        public string PrintedUser { get; set; }
        public DateTime? PrintedDate { get; set; }
    }
}
