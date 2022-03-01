using BNS.Resource.LocalizationResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BNS.Models.Requests
{
    public class CF_AccountModel
    {
    }

    public class CF_AccountRegisterModel : BaseRequestModel
    {
        [Display(Name = LocalizedBackendMessages.ShopName)]
        [Required(AllowEmptyStrings = false, ErrorMessage = LocalizedBackendMessages.MSG_CheckInput)]
        public string ShopName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = LocalizedBackendMessages.MSG_CheckInput)]
        [Display(Name = LocalizedBackendMessages.ShopCode)]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = LocalizedBackendMessages.MSG_InputOnlyCharactersAndNumber)]
        [MaxLength(25, ErrorMessage = LocalizedBackendMessages.MSG_InputMaxLengthCharacters)]
        public string ShopCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = LocalizedBackendMessages.MSG_CheckInput)]
        [Display(Name = LocalizedBackendMessages.Address)]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = LocalizedBackendMessages.MSG_CheckInput)]
        [Display(Name = LocalizedBackendMessages.Phone)]
        [MaxLength(30, ErrorMessage = LocalizedBackendMessages.MSG_InputMaxLengthCharacters)]
        [RegularExpression("^[0-9-]*$", ErrorMessage = LocalizedBackendMessages.MSG_InputOnlyNumberAndCharater)]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = LocalizedBackendMessages.MSG_CheckInput)]
        [Display(Name = LocalizedBackendMessages.UserName)]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = LocalizedBackendMessages.MSG_InputOnlyCharactersAndNumber)]
        [MaxLength(25, ErrorMessage = LocalizedBackendMessages.MSG_InputMaxLengthCharacters)]

        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = LocalizedBackendMessages.MSG_CheckInput)]
        [Display(Name = LocalizedBackendMessages.Password)]
        [DataType(DataType.Password)]
        [RegularExpression("^[a-zA-Z0-9!@#$^&*]*$", ErrorMessage = LocalizedBackendMessages.MSG_InputOnlyCharactersNumberAndSpecicalCharacter)]
        [MaxLength(25, ErrorMessage = LocalizedBackendMessages.MSG_InputMaxLengthCharacters)]
        [MinLength(6, ErrorMessage = LocalizedBackendMessages.MSG_InputMinLength6Characters)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = LocalizedBackendMessages.MSG_CheckInput)]
        [Display(Name = LocalizedBackendMessages.ConfirmPassword)]
        [DataType(DataType.Password)]
        [RegularExpression("^[a-zA-Z0-9!@#$^&*]*$", ErrorMessage = LocalizedBackendMessages.MSG_InputOnlyCharactersNumberAndSpecicalCharacter)]
        [MaxLength(25, ErrorMessage = LocalizedBackendMessages.MSG_InputMaxLengthCharacters)]
        [Compare(nameof(Password), ErrorMessage = LocalizedBackendMessages.MSG_ConfirmPasswordNotCorrect)]
        [MinLength(6, ErrorMessage = LocalizedBackendMessages.MSG_InputMinLength6Characters)]

        public string ConfirmPassword { get; set; }

        public string CaptchaCode { get; set; }
        public string CaptchaValue { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = LocalizedBackendMessages.MSG_CheckInput)]
        [Display(Name = LocalizedBackendMessages.CaptchaValue)]
        [MaxLength(4)]
        [Compare(nameof(CaptchaValue), ErrorMessage = LocalizedBackendMessages.MSG_CaptchaNotCorrect)]
        public string CaptchaRequestValue { get; set; }
        public byte[] Captcha { get; set; }


    }
    public class CF_AccountLoginModel 
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = LocalizedBackendMessages.MSG_CheckInput)]
        [Display(Name = LocalizedBackendMessages.UserName)]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = LocalizedBackendMessages.MSG_CheckInput)]
        [Display(Name = LocalizedBackendMessages.Password)]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = LocalizedBackendMessages.MSG_CheckInput)]
        [Display(Name = LocalizedBackendMessages.ShopCode)]
        public string ShopCode { get; set; }

        public bool Remember { get; set; }
        public string lang { get; set; }
    }
    public class CaptChaModel
    {
        public string CaptchaValue { get; set; }
        public string CaptchaCode { get; set; }
        public byte[] Captcha { get; set; }
    }

    public class CF_AccountUpdateBranchModel
    {
        public Guid UserIndex { get; set; }
        public List<Guid> Branchs { get; set; }
    }
}