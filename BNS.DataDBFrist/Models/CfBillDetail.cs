using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfBillDetail
    {
        public long Id { get; set; }
        public int? RoomIndex { get; set; }
        public int? ProductIndex { get; set; }
        public int? ShopIndex { get; set; }
        public int? BranchIndex { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public double? Cost { get; set; }
        public double? Sale { get; set; }
        public double? TotalMoney { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? BillId { get; set; }
    }
}
