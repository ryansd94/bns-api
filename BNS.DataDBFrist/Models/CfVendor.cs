using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfVendor
    {
        public int Index { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string TaxCode { get; set; }
        public int? VendorGroupIndex { get; set; }
        public string Note { get; set; }
        public int? ShopIndex { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public bool? Status { get; set; }
        public int? BranchIndex { get; set; }
    }
}
