using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_ProductSub
    {
        public Guid ProductSubIndex { get; set; }
        public Guid ProductIndex { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public Guid? ShopIndex { get; set; }
        public Guid? BranchIndex { get; set; }
    }
}
