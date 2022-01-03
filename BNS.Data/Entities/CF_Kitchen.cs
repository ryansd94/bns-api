using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_Kitchen
    {
        public Guid Index { get; set; }
        public int? OrderIndex { get; set; }
        public int? Quantity { get; set; }
        public bool? IsDone { get; set; }
        public string Note { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public int? QuantityWaitProcess { get; set; }
        public int? QuantityDone { get; set; }
        public int? QuantityUnProcess { get; set; }
        public DateTime? WaitProcessDate { get; set; }
        public DateTime? DoneDate { get; set; }
        public bool? IsPrioritize { get; set; }
        public Guid? ShopIndex { get; set; }
        public Guid? BranchIndex { get; set; }
        public int? QuantityFinished { get; set; }
        public DateTime? FinishedDate { get; set; }
    }
}
