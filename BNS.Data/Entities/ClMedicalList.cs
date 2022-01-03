using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class ClMedicalList
    {
        public long Index { get; set; }
        public long? PatientIndex { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string Code { get; set; }
        public int? Status { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? IsDelete { get; set; }
        public int? BranchIndex { get; set; }
        public int? ShopIndex { get; set; }
    }
}
