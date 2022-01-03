using BNS.Resource.LocalizationResources;
using BNS.ViewModels.Responses.Category;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.ViewModels.Requests
{
    public class CF_AreaModel: CategoryModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = LocalizedBackendMessages.MSG_CheckInput)]
        [Display(Name = LocalizedBackendMessages.AreaName)]
        public string Name { get; set; }
        public List<CategoryResponseModel> ListBranch { get; set; }

    }
}
