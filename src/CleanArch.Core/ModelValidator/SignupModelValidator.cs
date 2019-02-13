using CleanArch.Core.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.ModelValidator
{
   public class SignupModelValidator : AbstractValidator<SignupModel>
    {
        public SignupModelValidator()
        {
            RuleFor(reg => reg.FirstName).NotEmpty();
            RuleFor(reg => reg.LastName).NotEmpty();
            RuleFor(reg => reg.Email).NotEmpty().EmailAddress();
        }
    }
    
}
