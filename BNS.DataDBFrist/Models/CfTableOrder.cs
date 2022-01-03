using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfTableOrder
    {
        public int Index { get; set; }
        public int? RoomIndex { get; set; }
        public int? CustomerIndex { get; set; }
        public bool? IsPaid { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? ShopIndex { get; set; }
        public int? BranchIndex { get; set; }
        public bool? IsBook { get; set; }
        public bool? IsPrintBill { get; set; }
        public string PrintedUser { get; set; }
        public DateTime? PrintedDate { get; set; }
    }
}
