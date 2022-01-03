using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfPayment
    {
        public int Index { get; set; }
        public string Code { get; set; }
        public int? BranchIndex { get; set; }
        public string TypeName { get; set; }
        public string Payer { get; set; }
        public int? VendorIndex { get; set; }
        public int? CustomerIndex { get; set; }
        public string EmployeeCode { get; set; }
        public double? Value { get; set; }
        public string Note { get; set; }
        public int? Status { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? ShopIndex { get; set; }
        public DateTime? Date { get; set; }
        public int? Type { get; set; }
        public bool? IsExample { get; set; }
    }
}
