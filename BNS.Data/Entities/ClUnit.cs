﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class ClUnit
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string NameInEng { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int? ShopIndex { get; set; }
        public int? BranchIndex { get; set; }
        public bool? IsDelete { get; set; }
    }
}
