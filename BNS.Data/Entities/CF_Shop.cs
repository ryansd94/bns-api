using System;
using System.Collections.Generic;
using static BNS.Utilities.Enums;

#nullable disable

namespace BNS.Data.Entities
{
    public partial class CF_Shop
    {
        public Guid Index { get; set; }
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
        public EVersionType? VersionType { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string LicenseText { get; set; }
        public int? Module { get; set; }
        public ICollection<CF_Account> CF_Accounts { get; set; }
        public ICollection<CF_Employee> Cf_Employees { get; set; }
        public ICollection<CF_Area> CF_Areas { get; set; }
        public ICollection<CF_Department> CF_Departments { get; set; }
        public ICollection<CF_Position> CF_Positions { get; set; }
        public ICollection<CF_Branch> CF_Branchs { get; set; }
        public ICollection<Sys_RoleGroup> Sys_RoleGroups { get; set; }
    }
}
