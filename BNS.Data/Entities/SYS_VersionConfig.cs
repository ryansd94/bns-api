using System;
using System.Collections.Generic;
using System.Text;
using static BNS.Utilities.Enums;

namespace BNS.Data.Entities
{
    public partial class SYS_VersionConfig
    {
        public EVersionType VersionType { get; set; }
        public string Menu { get; set; }
    }
}
