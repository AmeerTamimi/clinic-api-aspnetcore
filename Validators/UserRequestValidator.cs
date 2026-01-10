using ClinicAPI.Requests;
using FluentValidation;

namespace ClinicAPI.Validators
{
    public abstract class UserRequestValidator<T> : AbstractValidator<T>
        where T : UserRequest
    {
        protected UserRequestValidator()
        {

            RuleFor(p => p.FirstName)
               .NotEmpty().WithMessage("You must fill the First Name")
               .Matches("^[A-Za-z]+$").WithMessage("First Name must contain letters only")
               .MaximumLength(255).WithMessage("First Name must be <= 255 characters");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("You must fill the Last Name")
                .Matches("^[A-Za-z]+$").WithMessage("Last Name must contain letters only")
                .MaximumLength(255).WithMessage("Last Name must be <= 255 characters");

            RuleFor(p => p.Age)
                .NotEmpty().WithMessage("You must fill the Age")
                .InclusiveBetween(0, 130).WithMessage("Age must be between 0-130");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("You must fill the Email")
                .EmailAddress().WithMessage("Invalid Email format")
                .MaximumLength(120).WithMessage("Email must be <= 120 characters");

            RuleFor(p => p.Phone)
                .NotEmpty().WithMessage("You must fill the Phone")
                .Matches(@"^\+?[0-9]{8,12}$").WithMessage("Phone must be 8-12 digits (optionally starts with +)");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("You must fill the Password")
                .Length(8,59).WithMessage("Password must be between 8 and 50 characters");
        }
    }
}
