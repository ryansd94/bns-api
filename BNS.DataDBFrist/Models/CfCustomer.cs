using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfCustomer
    {
        public int Index { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Cmnd { get; set; }
        public DateTime? BrithDate { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public int? CustomerType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string NameFacebook { get; set; }
        public string Gender { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? ShopIndex { get; set; }
        public int? IsDelete { get; set; }
        public int? BranchIndex { get; set; }
    }
}
