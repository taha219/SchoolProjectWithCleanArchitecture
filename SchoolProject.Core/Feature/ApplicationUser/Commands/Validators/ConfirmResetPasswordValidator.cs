using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.ApplicationUser.Commands.Validators
{
    public class ConfirmResetPasswordValidator : AbstractValidator<ConfirmResetPasswordCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public ConfirmResetPasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.PasswordRequired])
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,50}$")
                .WithMessage(_stringLocalizer[SharedResourcesKeys.PasswordNotValid]);
            RuleFor(x => x.ConfirmPassword)
                  .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.PasswordRequired])
                  .Equal(x => x.NewPassword).WithMessage(_stringLocalizer[SharedResourcesKeys.PasswordsDoNotMatch]);
        }
    }
}
