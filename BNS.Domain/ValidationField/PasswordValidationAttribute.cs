using BNS.Resource;
using BNS.Resource.LocalizationResources;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace BNS.Domain
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        readonly bool _isRequired;
        public PasswordValidationAttribute(bool IsRequired)
        {
            _isRequired = IsRequired;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IStringLocalizer<SharedResource> sharedLocalizer = (IStringLocalizer<SharedResource>)validationContext.GetService(typeof(IStringLocalizer<SharedResource>));
           
            if (value == null)
            {
                if (!_isRequired)
                    return ValidationResult.Success;
                return new ValidationResult(string.Format(sharedLocalizer[LocalizedBackendMessages.MSG_CheckInput], sharedLocalizer[LocalizedBackendMessages.Password] ));
            }
            var input = value.ToString().ToLower();
            if (string.IsNullOrWhiteSpace(input))
            {
                if (!_isRequired)
                    return ValidationResult.Success;
                return new ValidationResult(string.Format(sharedLocalizer[LocalizedBackendMessages.MSG_CheckInput], sharedLocalizer[LocalizedBackendMessages.Password]));
            }
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                return new ValidationResult(sharedLocalizer[LocalizedBackendMessages.User.MSG_PaswordShouldContainOneCharacter]);
            }
            //else if (!hasUpperChar.IsMatch(input))
            //{
            //    ErrorMessage = "Password should contain at least one upper case letter.";
            //    return false;
            //}
            //else if (!hasMiniMaxChars.IsMatch(input))
            //{
            //    ErrorMessage = "Password should not be lesser than 8 or greater than 15 characters.";
            //    return false;
            //}
            else if (!hasNumber.IsMatch(input))
            {
                return new ValidationResult(sharedLocalizer[LocalizedBackendMessages.User.MSG_PaswordShouldContainOneNumber]);
            }
            else if (input.Length < 6)
            {
                return new ValidationResult(string.Format(sharedLocalizer[LocalizedBackendMessages.User.MSG_PaswordLength], "6"));
            }
            //else if (!hasSymbols.IsMatch(input))
            //{
            //    ErrorMessage = "Password should contain at least one special case character.";
            //    return false;
            //}
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
