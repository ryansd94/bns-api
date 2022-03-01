using BNS.Models.Responses.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Models.Responses
{
    public class CF_AccountLoginResponseModel
    {
        public string UserName { get; set; }
        public string ShopCode { get; set; }

        public Guid ShopIndex { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public Guid? BranchIndex { get; set; }
        public bool MainAccount { get; set; }

        public List<Guid> Branchs { get; set; }

        public List<SYS_ColumnControlResponseModel> ColumnControls { get; set; }
        public List<MenuResponseModel> Menus { get; set; }
    }

}
