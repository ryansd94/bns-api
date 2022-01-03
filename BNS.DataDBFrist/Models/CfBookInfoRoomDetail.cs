using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfBookInfoRoomDetail
    {
        public int Index { get; set; }
        public int? BookIndex { get; set; }
        public int? RoomIndex { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? ShopIndex { get; set; }
        public int? BranchIndex { get; set; }
        public int? IsDelete { get; set; }
    }
}
