using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_Vendor
    {
        public Guid Index { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string TaxCode { get; set; }
        public Guid? VendorGroupIndex { get; set; }
        public string Note { get; set; }
        public Guid? ShopIndex { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public bool? Status { get; set; }
        public Guid? BranchIndex { get; set; }
    }
}
