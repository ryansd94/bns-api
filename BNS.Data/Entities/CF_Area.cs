using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_Area: BaseEntity
    {
        public string Name { get; set; }
        public string NameInEng { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int? IsExample { get; set; }
        public int? Number { get; set; }
        public CF_Shop CF_Shop { get; set; }
        public CF_Branch CF_Branch { get; set; }
    }
}
