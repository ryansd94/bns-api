using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_Decentralization
    {
        public Guid Index { get; set; }
        public Guid? ShopIndex { get; set; }
        public string Page { get; set; }
        public string ByEmployeeCode { get; set; }
        public Guid? ByPosition { get; set; }
        public Guid? ByDepartment { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? BranchIndex { get; set; }
    }
}
