using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.Students.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Feature.Students.Commands.Validators
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public AddStudentValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            this._stringLocalizer = stringLocalizer;
            RuleFor(x => x.NameAr).NotEmpty()
                .WithMessage(_stringLocalizer[SharedResourcesKeys.NameRequired])
                .MaximumLength(20)
                .WithMessage(_stringLocalizer[SharedResourcesKeys.Namelength]);
            RuleFor(x => x.NameEn).NotEmpty()
               .WithMessage(_stringLocalizer[SharedResourcesKeys.NameRequired])
               .MaximumLength(20)
               .WithMessage(_stringLocalizer[SharedResourcesKeys.Namelength]);
            RuleFor(x => x.AddressAr)
                .NotEmpty()
                .WithMessage(_stringLocalizer[SharedResourcesKeys.AddressRequired])
                .MaximumLength(20)
                .WithMessage(_stringLocalizer[SharedResourcesKeys.Addresslength]);
            RuleFor(x => x.AddressEn)
               .NotEmpty()
               .WithMessage(_stringLocalizer[SharedResourcesKeys.AddressRequired])
               .MaximumLength(20)
               .WithMessage(_stringLocalizer[SharedResourcesKeys.Addresslength]);
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage(_stringLocalizer[SharedResourcesKeys.PhoneRequired])
                .Matches(@"^01[0125][0-9]{8}$")
                .WithMessage(_stringLocalizer[SharedResourcesKeys.PhoneVaild]);
        }

    }
}
