using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class ClPrescription
    {
        public long Index { get; set; }
        public string PrescriptionCode { get; set; }
        public long? PatientIndex { get; set; }
        public long? MedicalListIndex { get; set; }
        public decimal? ExaminaMoney { get; set; }
        public decimal? ExaminaMedicine { get; set; }
        public decimal? TotalMoney { get; set; }
        public DateTime? NgayKhamBenh { get; set; }
        public string TrieuChung { get; set; }
        public long? BacSyKhamIndex { get; set; }
        public string Note { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? IsDelete { get; set; }
        public int? BranchIndex { get; set; }
        public int? ShopIndex { get; set; }
        public string ChanDoan { get; set; }
    }
}
