using BNS.Resource.LocalizationResources;
using BNS.ViewModels.Responses.Category;
using BNS.ViewModels.ValidationModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.ViewModels.Requests
{
    public class CF_EmployeeModel : BaseRequestModel
    {
        [Required(AllowEmptyStrings = true, ErrorMessage = "Vui lòng không bỏ trống các thông tin có dấu *")]
        public string EmployeeName { get; set; }
        [MaxLength(30, ErrorMessage = LocalizedBackendMessages.MSG_InputMaxLengthCharacters)]
        public string EmployeeCode { get; set; }
        public int? Gender { get; set; }
        [MaxLength(20, ErrorMessage = LocalizedBackendMessages.MSG_InputMaxLengthCharacters)]
        public string Nric { get; set; }
        public DateTime? DateOfNric { get; set; }
        public string PlaceOfNric { get; set; }
        public DateTime? BrithDate { get; set; }
        public DateTime? JoinedDate { get; set; }
        public string PermanentAddress { get; set; }
        public string TemporaryAddress { get; set; }
        public bool? Active { get; set; }
        [MaxLength(30, ErrorMessage = LocalizedBackendMessages.MSG_InputMaxLengthCharacters)]
        public string Email { get; set; }
        [MaxLength(30, ErrorMessage = LocalizedBackendMessages.MSG_InputMaxLengthCharacters)]
        public string Phone { get; set; }
        public string EmployeeImage { get; set; }
        public bool? IsMainAccount { get; set; }
        public int? Region { get; set; }

        [MaxLength(25, ErrorMessage = LocalizedBackendMessages.MSG_InputMaxLengthCharacters)]
        public string UserName { get; set; }
        [Display(Name = LocalizedBackendMessages.Password)]
        [DataType(DataType.Password)]
        [RegularExpression("^[a-zA-Z0-9!@#$^&*]*$", ErrorMessage = LocalizedBackendMessages.MSG_InputOnlyCharactersNumberAndSpecicalCharacter)]
        [MaxLength(50, ErrorMessage = LocalizedBackendMessages.MSG_InputMaxLengthCharacters)]
        [MinLength(6, ErrorMessage = LocalizedBackendMessages.MSG_InputMinLength6Characters)]
        public string Password { get; set; }

        [Display(Name = LocalizedBackendMessages.ConfirmPassword)]
        [DataType(DataType.Password)]
        [RegularExpression("^[a-zA-Z0-9!@#$^&*]*$", ErrorMessage = LocalizedBackendMessages.MSG_InputOnlyCharactersNumberAndSpecicalCharacter)]
        [MaxLength(50, ErrorMessage = LocalizedBackendMessages.MSG_InputMaxLengthCharacters)]
        [Compare(nameof(Password), ErrorMessage = LocalizedBackendMessages.MSG_ConfirmPasswordNotCorrect)]
        [MinLength(6, ErrorMessage = LocalizedBackendMessages.MSG_InputMinLength6Characters)]

        public string ConfirmPassword { get; set; }
        [MaxFileSize]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Files { get; set; }
        public String StrFiles { get; set; }
        public DateTime? WorkingDate { get; set; }
        public Guid? DepartmentIndex { get; set; }
        public Guid? PositionIndex { get; set; }

        public List<CategoryResponseModel> ListDepartment { get; set; }
        public List<CategoryResponseModel> ListPosition { get; set; }
        public List<CategoryResponseModel> ListBranch { get; set; }

        public CF_EmployeeSearchModel SearchModel { get; set; }
    }

    public class CF_EmployeeSearchModel
    {
        public string Keyword { get; set; }
        public List<string> StrDepartmentIndex { get; set; }
        public List<string> StrPositionIndex { get; set; }
        public IList<string> Gender { get; set; }
        public DateTime? WorkingDateFrom { get; set; }
        public DateTime? WorkingDateTo { get; set; }
        public List<CategoryResponseModel> ListDepartment { get; set; }
        public List<CategoryResponseModel> ListPosition { get; set; }
    }
}
