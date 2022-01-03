using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_Shift
    {
        public Guid Index { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public Guid? ShopIndex { get; set; }
        public int? IsExample { get; set; }
        public int? IsDelete { get; set; }
        public Guid? BranchIndex { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
    }
}
