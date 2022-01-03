using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_Room
    {
        public Guid Index { get; set; }
        public string Name { get; set; }
        public string NameInEng { get; set; }
        public Guid? AreaIndex { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public int? Status { get; set; }
        public Guid? ShopIndex { get; set; }
        public int? IsExample { get; set; }
        public int? IsDelete { get; set; }
        public Guid? BranchIndex { get; set; }
    }
}
