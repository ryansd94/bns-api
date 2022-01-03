using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_History
    {
        public Guid Index { get; set; }
        public string EmployeeCode { get; set; }
        public string FormName { get; set; }
        public string Note { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public Guid? ShopIndex { get; set; }
        public string Function { get; set; }
        public Guid? BranchIndex { get; set; }
    }
}
