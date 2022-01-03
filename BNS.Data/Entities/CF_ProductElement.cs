using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_ProductElement
    {
        public Guid ProductEleIndex { get; set; }
        public Guid ProductIndex { get; set; }
        public double? Quantity { get; set; }
        public double? Cost { get; set; }
        public double? TotalMoney { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public Guid? ShopIndex { get; set; }
        public Guid? BranchIndex { get; set; }
    }
}
