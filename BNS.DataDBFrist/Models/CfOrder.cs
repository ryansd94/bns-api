using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfOrder
    {
        public int Index { get; set; }
        public int? ProductIndex { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public double? Sale { get; set; }
        public double? TotalMoney { get; set; }
        public bool? IsAnnounced { get; set; }
        public bool? IsDone { get; set; }
        public string Note { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? QuantityOld { get; set; }
        public bool? IsInvalid { get; set; }
        public int? QuantityAnnounced { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsPrioritize { get; set; }
        public int? ShopIndex { get; set; }
        public int? TableOrderIndex { get; set; }
        public int? BranchIndex { get; set; }
        public double? Cost { get; set; }
        public bool? IsBook { get; set; }
    }
}
