using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfBookInfo
    {
        public int Index { get; set; }
        public int? CustomerIndex { get; set; }
        public int? RoomIndex { get; set; }
        public DateTime? DateBook { get; set; }
        public string NumberPeople { get; set; }
        public string Note { get; set; }
        public int? Status { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? ShopIndex { get; set; }
        public int? IsDelete { get; set; }
        public int? BranchIndex { get; set; }
        public bool? IsBookDish { get; set; }
        public long? TableOrderIndex { get; set; }
    }
}
