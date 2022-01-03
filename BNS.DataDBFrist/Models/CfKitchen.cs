using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfKitchen
    {
        public int Index { get; set; }
        public int? OrderIndex { get; set; }
        public int? Quantity { get; set; }
        public bool? IsDone { get; set; }
        public string Note { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? QuantityWaitProcess { get; set; }
        public int? QuantityDone { get; set; }
        public int? QuantityUnProcess { get; set; }
        public DateTime? WaitProcessDate { get; set; }
        public DateTime? DoneDate { get; set; }
        public bool? IsPrioritize { get; set; }
        public int? ShopIndex { get; set; }
        public int? BranchIndex { get; set; }
        public int? QuantityFinished { get; set; }
        public DateTime? FinishedDate { get; set; }
    }
}
