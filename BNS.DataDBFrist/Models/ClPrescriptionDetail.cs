using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class ClPrescriptionDetail
    {
        public long Index { get; set; }
        public long? PrescriptionIndex { get; set; }
        public long? PatientIndex { get; set; }
        public long? MedicalListIndex { get; set; }
        public long? MedicineIndex { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalMoney { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? IsDelete { get; set; }
        public int? BranchIndex { get; set; }
        public int? ShopIndex { get; set; }
    }
}
