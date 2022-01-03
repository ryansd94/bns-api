using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfDiscount
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Note { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string DateApplyType { get; set; }
        public bool? ForMon { get; set; }
        public bool? ForTue { get; set; }
        public bool? ForWed { get; set; }
        public bool? ForThu { get; set; }
        public bool? ForFri { get; set; }
        public bool? ForSat { get; set; }
        public bool? ForSun { get; set; }
        public string ObjectType { get; set; }
        public string ReduceType { get; set; }
        public double? RatioReduce { get; set; }
        public double? MoneyReduce { get; set; }
        public bool? ApplyForBill { get; set; }
        public double? MoneyBill { get; set; }
        public bool? ApplyForProductGroup { get; set; }
        public bool? ApplyForProduct { get; set; }
        public string ProductGroupValue { get; set; }
        public string ProductValue { get; set; }
        public int? BranchIndex { get; set; }
        public int? ShopIndex { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? IsDelete { get; set; }
        public bool? ApplyTime { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }
    }
}
