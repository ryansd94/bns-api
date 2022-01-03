using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_BillDetail
    {
        public Guid Id { get; set; }
        public Guid RoomIndex { get; set; }
        public Guid ProductIndex { get; set; }
        public Guid ShopIndex { get; set; }
        public Guid BranchIndex { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public double? Cost { get; set; }
        public double? Sale { get; set; }
        public double? TotalMoney { get; set; }
        public DateTime? OrderDate { get; set; }
        public Guid OrderUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid UpdatedUser { get; set; }
        public Guid? BillId { get; set; }
    }
}
