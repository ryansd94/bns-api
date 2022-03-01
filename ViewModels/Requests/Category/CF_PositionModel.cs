using BNS.Resource.LocalizationResources;
using System.ComponentModel.DataAnnotations;

namespace BNS.Models.Requests
{
    public class CF_PositionModel : CategoryModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = LocalizedBackendMessages.MSG_CheckInput)]
        [Display(Name = LocalizedBackendMessages.DepartmentName)]
        public string Name { get; set; }
    }
}
