using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class Cf_SecurityQuestion
    {
        public Guid Index { get; set; }
        public string Question { get; set; }
        public Guid? ShopIndex { get; set; }
    }
}
