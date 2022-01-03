using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_PurchaseOrderDetail
    {
        public Guid Index { get; set; }
        public Guid? PurchaseOrderIndex { get; set; }
        public Guid? ProductIndex { get; set; }
        public double? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Sale { get; set; }
        public decimal? TotalMoney { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public Guid? ShopIndex { get; set; }
        public decimal? ImportPrice { get; set; }
        public string Note { get; set; }
        public Guid? BranchIndex { get; set; }
    }
}
