using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfSecurityQuestion
    {
        public int Index { get; set; }
        public string Question { get; set; }
        public int? ShopIndex { get; set; }
    }
}
