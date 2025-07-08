using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.Email.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.ApplicationUser.Command.Validators
{
    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
    {
        #region fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region ctor
        public SendEmailCommandValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Messege).NotEmpty()
               .WithMessage(_stringLocalizer[SharedResourcesKeys.MessegeRequired]);


            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(_stringLocalizer[SharedResourcesKeys.EmailRequired])
                .Matches(@"^[^@\s]+@(gmail\.com|outlook\.com|yahoo\.com)$").WithMessage(_stringLocalizer[SharedResourcesKeys.EmailNotVaild]);

        }
        #endregion



    }
}
