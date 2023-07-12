using System.Collections.Generic;
using static BNS.Utilities.Enums;

namespace BNS.Domain.Commands
{
    public class ColumnItemRoot
    {
        public List<ColumnItem> column1 { get; set; }
        public List<ColumnItem> column2 { get; set; }
        public List<ColumnItem> column3 { get; set; }
    }
    public class ColumnItem
    {
        public string id { get; set; }
        public string customColumnId { get; set; }
        public string type { get; set; }
        public string label { get; set; }
        public string name { get; set; }
        public bool isAddNew { get; set; }
        public string columnId { get; set; }
        public bool @default { get; set; }
        public string prefix { get; set; }
        public bool defaultReadonly { get; set; }
        public bool? required { get; set; }
        public bool isHidenWhenCreate { get; set; }
        public List<ColumnItem> items { get; set; }
    }
    public class ColumnObject
    {
        public EColumnPosition ColumnPosition { get; set; }
        public List<ColumnItem> Column { get; set; }
    }
}
