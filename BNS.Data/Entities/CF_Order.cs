using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_Order
    {
        public Guid Index { get; set; }
        public Guid? ProductIndex { get; set; }
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
        public Guid? ShopIndex { get; set; }
        public Guid? TableOrderIndex { get; set; }
        public Guid? BranchIndex { get; set; }
        public double? Cost { get; set; }
        public bool? IsBook { get; set; }
        public Guid UpdatedUserId { get; set; }
        //public CF_Account CF_Account { get; set; }
    }
}
