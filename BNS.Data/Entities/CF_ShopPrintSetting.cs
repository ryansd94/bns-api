using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_ShopPrintSetting
    {
        public Guid ShopIndex { get; set; }
        public string PrintBillName { get; set; }
        public string PrintBillIp { get; set; }
        public string PrintBillPort { get; set; }
        public string ServiceIp { get; set; }
        public string ServicePort { get; set; }
        public Guid? BranchIndex { get; set; }
        public Guid Index { get; set; }
        public int? PrintType { get; set; }
        public bool? IsPayAndPrintBill { get; set; }
        public string Note { get; set; }
    }
}
