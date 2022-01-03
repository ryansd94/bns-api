using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_PurchaseOrder
    {
        public Guid Index { get; set; }
        public string Code { get; set; }
        public Guid? VendorIndex { get; set; }
        public Guid? BranchIndex { get; set; }
        public double? Quantity { get; set; }
        public double? QuantityProduct { get; set; }
        public decimal? TotalMoney { get; set; }
        public decimal? Sale { get; set; }
        public decimal? ShoudPayVendor { get; set; }
        public decimal? PayVendor { get; set; }
        public int? Status { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public string Note { get; set; }
        public Guid? ShopIndex { get; set; }
        public DateTime? Date { get; set; }
        public int? Type { get; set; }
        public decimal? VendorShoudPay { get; set; }
        public decimal? VendorPay { get; set; }
    }
}
