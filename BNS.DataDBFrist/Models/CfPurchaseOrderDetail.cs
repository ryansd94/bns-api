using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfPurchaseOrderDetail
    {
        public int Index { get; set; }
        public int? PurchaseOrderIndex { get; set; }
        public int? ProductIndex { get; set; }
        public double? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Sale { get; set; }
        public decimal? TotalMoney { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? ShopIndex { get; set; }
        public decimal? ImportPrice { get; set; }
        public string Note { get; set; }
        public int? BranchIndex { get; set; }
    }
}
