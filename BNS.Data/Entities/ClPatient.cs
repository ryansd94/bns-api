using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class ClPatient
    {
        public long Index { get; set; }
        public string Name { get; set; }
        public string ExaminaCode { get; set; }
        public string Code { get; set; }
        public int? Gender { get; set; }
        public string Cmnd { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? IsDelete { get; set; }
        public int? BranchIndex { get; set; }
        public int? ShopIndex { get; set; }
    }
}
