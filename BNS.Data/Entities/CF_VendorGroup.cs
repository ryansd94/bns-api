using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_VendorGroup
    {
        public Guid Index { get; set; }
        public string Name { get; set; }
        public string NameInEng { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public Guid? ShopIndex { get; set; }
        public Guid? BranchIndex { get; set; }
    }
}
