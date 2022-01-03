using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfShopSetting
    {
        public int ShopIndex { get; set; }
        public bool? MsgGoiMonKhiHetTonKho { get; set; }
        public bool? MsgThanhToanCacMonDaOrder { get; set; }
        public bool? MsgKhongSuDungTinhNangBep { get; set; }
        public int? BranchIndex { get; set; }
        public bool? MsgTatCaHangHoaChuyenBepKhiThongBao { get; set; }
        public bool? MsgKiemTraCaLamViec { get; set; }
        public int Index { get; set; }
        public bool? MsgChanIpngoaiDanhSach { get; set; }
    }
}
