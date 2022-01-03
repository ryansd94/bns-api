using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfEmployeeWorkingInfo
    {
        public int Index { get; set; }
        public string EmployeeCode { get; set; }
        public int? PositionIndex { get; set; }
        public int? DepartmentIndex { get; set; }
        public int? BranchIndex { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Note { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? ShopIndex { get; set; }
    }
}
