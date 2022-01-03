using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_BookInfo
    {
        public Guid Index { get; set; }
        public Guid? CustomerIndex { get; set; }
        public Guid? RoomIndex { get; set; }
        public DateTime? DateBook { get; set; }
        public string NumberPeople { get; set; }
        public string Note { get; set; }
        public int? Status { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public Guid? ShopIndex { get; set; }
        public int? IsDelete { get; set; }
        public Guid? BranchIndex { get; set; }
        public bool? IsBookDish { get; set; }
        public Guid? TableOrderIndex { get; set; }
    }
}
