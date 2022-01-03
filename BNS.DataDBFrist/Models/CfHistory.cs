using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfHistory
    {
        public int Index { get; set; }
        public string EmployeeCode { get; set; }
        public string FormName { get; set; }
        public string Note { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? ShopIndex { get; set; }
        public string Function { get; set; }
        public int? BranchIndex { get; set; }
    }
}
