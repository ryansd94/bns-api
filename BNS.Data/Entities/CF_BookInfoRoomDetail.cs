using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_BookInfoRoomDetail
    {
        public Guid Index { get; set; }
        public Guid? BookIndex { get; set; }
        public Guid? RoomIndex { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public Guid? ShopIndex { get; set; }
        public Guid? BranchIndex { get; set; }
        public int? IsDelete { get; set; }
    }
}
