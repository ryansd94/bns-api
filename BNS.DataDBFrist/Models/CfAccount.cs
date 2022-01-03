using System;
using System.Collections.Generic;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class CfAccount
    {
        public string EmployeeCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public int ShopIndex { get; set; }
        public bool? IsMainAccount { get; set; }
        public int Index { get; set; }
        public string ShopCode { get; set; }
    }
}
