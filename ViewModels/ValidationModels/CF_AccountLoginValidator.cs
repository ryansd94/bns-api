using BNS.Models.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Models.ValidationModels
{
   public class CF_AccountLoginValidator: AbstractValidator<CF_AccountLoginModel>
    {
        public CF_AccountLoginValidator()
        {
            RuleFor(s => s.UserName).NotEmpty().WithMessage("Tài khoản không được trống");
        }
    }
}
