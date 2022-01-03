using System;
using System.Collections.Generic;
using System.Text;
using static BNS.Utilities.Enums;

namespace BNS.ViewModels.Responses
{
   public class Sys_RoleResponseModel : BaseResultModel
    {
        public string Permission { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }

    }
    public class Sys_RoleClaimResponseModel : BaseResultModel
    {
        public List<Sys_RoleResponseModel> Roles { get; set; }
        public Guid DataId { get; set; }
        public ERoleType Type { get; set; }

    }
}
