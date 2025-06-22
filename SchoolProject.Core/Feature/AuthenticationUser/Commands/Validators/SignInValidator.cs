using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.AuthenticationUser.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.AuthenticationUser.Commands.Validators
{
    public class SignInValidators : AbstractValidator<SignInCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public SignInValidators(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.UserNameOrEmail)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
        #endregion
    }
}