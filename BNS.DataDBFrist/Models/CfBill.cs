using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfBill
    {
        public int Index { get; set; }
        public string Code { get; set; }
        public int? Quantity { get; set; }
        public double? TotalMoney { get; set; }
        public double? Sale { get; set; }
        public double? CustomerNeedPay { get; set; }
        public double? CustomerPaying { get; set; }
        public string OrderIndex { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? ShopIndex { get; set; }
        public int? BranchIndex { get; set; }
        public DateTime? Date { get; set; }
        public double? TotalCost { get; set; }
        public bool? IsExample { get; set; }
        public int? RoomIndex { get; set; }
        public bool? IsPayment { get; set; }
        public string PaymentUser { get; set; }
        public long? CustomerIndex { get; set; }
        public long? TableOrderIndex { get; set; }
    }
}
