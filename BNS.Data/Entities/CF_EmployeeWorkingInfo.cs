using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_EmployeeWorkingInfo
    {
        public Guid Index { get; set; }
        public Guid EmployeeIndex { get; set; }
        public Guid? PositionIndex { get; set; }
        public Guid? DepartmentIndex { get; set; }
        public Guid? BranchIndex { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Note { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public Guid? ShopIndex { get; set; }
    }
}
