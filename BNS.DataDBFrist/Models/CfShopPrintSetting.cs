using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfShopPrintSetting
    {
        public int ShopIndex { get; set; }
        public string PrintBillName { get; set; }
        public string PrintBillIp { get; set; }
        public string PrintBillPort { get; set; }
        public string ServiceIp { get; set; }
        public string ServicePort { get; set; }
        public int? BranchIndex { get; set; }
        public int Index { get; set; }
        public int? PrintType { get; set; }
        public bool? IsPayAndPrintBill { get; set; }
        public string Note { get; set; }
    }
}
