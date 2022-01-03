using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfDecentralization
    {
        public int Index { get; set; }
        public int? ShopIndex { get; set; }
        public string Page { get; set; }
        public string ByEmployeeCode { get; set; }
        public int? ByPosition { get; set; }
        public int? ByDepartment { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? BranchIndex { get; set; }
    }
}
