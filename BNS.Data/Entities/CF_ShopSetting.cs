using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_ShopSetting
    {
        public Guid ShopIndex { get; set; }
        public bool IsGoiMonKhiHetTonKho { get; set; }
        public bool IsThanhToanCacMonDaOrder { get; set; }
        public bool MsgKhongSuDungTinhNangBep { get; set; }
        public Guid? BranchIndex { get; set; }
        public bool MsgTatCaHangHoaChuyenBepKhiThongBao { get; set; }
        public bool MsgKiemTraCaLamViec { get; set; }
        public Guid Index { get; set; }
        public bool MsgChanIpngoaiDanhSach { get; set; }
    }
}
