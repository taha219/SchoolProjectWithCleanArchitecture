using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.ApplicationUser.Command.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.ApplicationUser.Command.Validators
{
    public class AddAppUserValidator : AbstractValidator<AddAppUserCommand>
    {
        #region fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region ctor
        public AddAppUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
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
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.PasswordRequired])
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,50}$")
                .WithMessage(_stringLocalizer[SharedResourcesKeys.PasswordNotValid]);
        }
        #endregion



    }
}
