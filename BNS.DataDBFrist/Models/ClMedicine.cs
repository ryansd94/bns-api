using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class ClMedicine
    {
        public long Index { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? MedicineType { get; set; }
        public decimal? Price { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Inventory { get; set; }
        public int? UnitIndex { get; set; }
        public decimal? RestMin { get; set; }
        public decimal? RestMax { get; set; }
        public string Note { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? ShopIndex { get; set; }
        public int? IsExample { get; set; }
        public int? IsDelete { get; set; }
        public int? BranchIndex { get; set; }
        public string Used { get; set; }
    }
}
