using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.ApplicationUser.Commands.Validators
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {

        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public ChangeUserPasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.PasswordRequired])
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,50}$")
                .WithMessage(_stringLocalizer[SharedResourcesKeys.PasswordNotValid]);

        }
    }
}