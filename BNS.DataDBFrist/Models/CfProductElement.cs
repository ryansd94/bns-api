using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfProductElement
    {
        public int ProductEleIndex { get; set; }
        public int ProductIndex { get; set; }
        public double? Quantity { get; set; }
        public double? Cost { get; set; }
        public double? TotalMoney { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? ShopIndex { get; set; }
        public int? BranchIndex { get; set; }
    }
}
