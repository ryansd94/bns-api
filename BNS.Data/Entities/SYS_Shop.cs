using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Data.Entities
{
    public class SYS_Shop
    {
        public long Index { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Code { get; set; }

        public int? NumberOfBranch { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string LogoImage { get; set; }

        public bool? IsCreatedDataExample { get; set; }

        public bool? IsShowCreateDataExampleMenu { get; set; }

        public bool? NotShowDataExample { get; set; }

        public bool? NotShowHelp { get; set; }

        public int? VersionType { get; set; }

        public DateTime RegisterDate { get; set; }

        public string LicenseText { get; set; }

        public int? Module { get; set; }

        public List<SYS_Account> SYS_Accounts { get; set; }

    }
}
