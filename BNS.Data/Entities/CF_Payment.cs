using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_Payment
    {
        public Guid Index { get; set; }
        public string Code { get; set; }
        public Guid? BranchIndex { get; set; }
        public string TypeName { get; set; }
        public string Payer { get; set; }
        public Guid? VendorIndex { get; set; }
        public Guid? CustomerIndex { get; set; }
        public string EmployeeCode { get; set; }
        public double? Value { get; set; }
        public string Note { get; set; }
        public int? Status { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? ShopIndex { get; set; }
        public DateTime? Date { get; set; }
        public int? Type { get; set; }
        public bool? IsExample { get; set; }
    }
}
