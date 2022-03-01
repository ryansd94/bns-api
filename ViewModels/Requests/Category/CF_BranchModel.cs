using BNS.Resource.LocalizationResources;
using System.ComponentModel.DataAnnotations;

namespace BNS.Models.Requests
{
    public class CF_BranchModel : CategoryModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = LocalizedBackendMessages.MSG_CheckInput)]
        [Display(Name = LocalizedBackendMessages.BranchName)]
        public string Name { get; set; }
        [MaxLength(250, ErrorMessage = LocalizedBackendMessages.MSG_InputMaxLengthCharacters)]
        public string Address { get; set; }
        [MaxLength(20, ErrorMessage = LocalizedBackendMessages.MSG_InputMaxLengthCharacters)]
        public string Phone { get; set; }
    }
}
