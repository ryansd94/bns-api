using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_Branch
    {
        public Guid Index { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUser { get; set; }
        public Guid ShopIndex { get; set; }
        public bool? IsDelete { get; set; }
        public string Name { get; set; }
        public string NameInEng { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? IsExample { get; set; }
        public int? Number { get; set; }
        public CF_Shop CF_Shop { get; set; }
        public bool? IsMaster { get; set; }
        public bool? IsDefault { get; set; }
        public ICollection<CF_Area> CF_Areas { get; set; }
        public ICollection<CF_Employee> CF_Employees { get; set; }
    }
}
