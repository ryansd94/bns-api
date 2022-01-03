using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Data.Entities
{
    public class SYS_Account
    {
        public string EmployeeCode { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int? SecurityQuestion { get; set; }

        public string SecurityAnswer { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedUser { get; set; }

        public long ShopIndex { get; set; }

        public bool? IsMainAccount { get; set; }

        public long Index { get; set; }

        public string ShopCode { get; set; }
        public SYS_Shop SYS_Shop { get; set; }

    }
}
