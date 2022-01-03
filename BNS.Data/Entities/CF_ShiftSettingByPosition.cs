using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_ShiftSettingByPosition
    {
        public Guid Id { get; set; }
        public Guid? ShiftId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string DateApply { get; set; }
        public Guid? PositionId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public Guid? ShopIndex { get; set; }
    }
}
