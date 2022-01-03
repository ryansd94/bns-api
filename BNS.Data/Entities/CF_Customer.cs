using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_Customer
    {
        public Guid Index { get; set; }
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
        public Guid? UpdatedUser { get; set; }
        public Guid? ShopIndex { get; set; }
        public int? IsDelete { get; set; }
        public Guid? BranchIndex { get; set; }
    }
}
