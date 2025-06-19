using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.ApplicationUser.Commands.Validators
{
    public class EditUserValidator : AbstractValidator<EditUserCommand>
    {
        #region fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region ctor
        public EditUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.UserName).NotEmpty()
               .WithMessage(_stringLocalizer[SharedResourcesKeys.NameRequired])
               .MaximumLength(20)
               .WithMessage(_stringLocalizer[SharedResourcesKeys.Namelength]);
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage(_stringLocalizer[SharedResourcesKeys.PhoneRequired])
                .Matches(@"^01[0125][0-9]{8}$")
                .WithMessage(_stringLocalizer[SharedResourcesKeys.PhoneNotVaild]);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.EmailRequired])
                .Matches(@"^[^@\s]+@(gmail\.com|outlook\.com|yahoo\.com)$").WithMessage(_stringLocalizer[SharedResourcesKeys.EmailNotVaild]);

        }
        #endregion
    }
}
