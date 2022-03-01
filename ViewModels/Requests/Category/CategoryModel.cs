using BNS.Resource.LocalizationResources;
using BNS.Models.ValidationModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace BNS.Models.Requests
{
    public class CategoryModel : BaseRequestModel
    {
        [Range(-1 * int.MaxValue, int.MaxValue, ErrorMessage = LocalizedBackendMessages.MSG_InputNumberNotValid)]
        [Display(Name = LocalizedBackendMessages.Number)]
        [Integer(ErrorMessage = "some message here")]
        public int? Number { get; set; }
        public string Note { get; set; }
    }
}
