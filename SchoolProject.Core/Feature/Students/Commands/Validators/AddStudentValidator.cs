using FluentValidation;
using SchoolProject.Core.Feature.Students.Commands.Models;

namespace SchoolProject.Core.Feature.Students.Commands.Validators
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        public AddStudentValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(20)
                .WithMessage("Name must not exceed 20 characters long");
            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address is required")
                .MaximumLength(20)
                .WithMessage("Address must not exceed 50 characters long");
            RuleFor(x => x.Phone).NotEmpty()
                .WithMessage("Phone is required")
                .Matches(@"^01[0125][0-9]{8}$")
                .WithMessage("Phone must be a 11 & egyptian number");
        }

    }
}
