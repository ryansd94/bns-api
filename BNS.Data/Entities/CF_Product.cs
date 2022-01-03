using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_Product
    {
        public Guid Index { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ProductType { get; set; }
        public Guid? GroupIndex { get; set; }
        public double? Price { get; set; }
        public double? Cost { get; set; }
        public double? Inventory { get; set; }
        public Guid? UnitIndex { get; set; }
        public bool? IsDirectSales { get; set; }
        public bool? IsSubElement { get; set; }
        public int? IsBusiness { get; set; }
        public double? RestMin { get; set; }
        public double? RestMax { get; set; }
        public string Note { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public double? InventoryTemp { get; set; }
        public double? InventoryRest { get; set; }
        public double? InventoryRestOld { get; set; }
        public Guid? ShopIndex { get; set; }
        public int? IsExample { get; set; }
        public int? IsDelete { get; set; }
        public Guid? BranchIndex { get; set; }
    }
}
