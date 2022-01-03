using BNS.Resource.LocalizationResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BNS.Utilities.Enums;

namespace BNS.ViewModels.Requests
{
    public class Sys_RoleModel : CategoryModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = LocalizedBackendMessages.MSG_CheckInput)]
        [Display(Name = LocalizedBackendMessages.AreaName)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Permission { get; set; }
    }
    public class Sys_DecentralizeModel : BaseRequestModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = LocalizedBackendMessages.MSG_CheckInput)]
        public Guid RoleId { get; set; }
        public List<string> LstKeyRole { get; set; }
    }
    public class Sys_RoleClaimModel : BaseRequestModel
    {
        public List<Guid> RoleId { get; set; }
        public Guid DataId { get; set; }
        public ERoleType Type { get; set; }
    }
}
