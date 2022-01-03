using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_Bill
    {
        public Guid Index { get; set; }
        public string Code { get; set; }
        public int? Quantity { get; set; }
        public double? TotalMoney { get; set; }
        public double? Sale { get; set; }
        public double? CustomerNeedPay { get; set; }
        public double? CustomerPaying { get; set; }
        public string OrderIndex { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public Guid UpdatedUserId { get; set; }
        public Guid? ShopIndex { get; set; }
        public Guid? BranchIndex { get; set; }
        public DateTime? Date { get; set; }
        public double? TotalCost { get; set; }
        public bool? IsExample { get; set; }
        public Guid? RoomIndex { get; set; }
        public bool? IsPayment { get; set; }
        public string PaymentUser { get; set; }
        public long? CustomerIndex { get; set; }
        public long? TableOrderIndex { get; set; }

        //public CF_Account CF_Account { get; set; }
    }
}
