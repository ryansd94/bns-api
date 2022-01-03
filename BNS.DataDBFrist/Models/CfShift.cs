using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfShift
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? ShopIndex { get; set; }
        public int? IsExample { get; set; }
        public int? IsDelete { get; set; }
        public int? BranchIndex { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
    }
}
