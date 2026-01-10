using ClinicAPI.Requests;
using FluentValidation;

namespace ClinicAPI.Validators
{
    public class JwtTokenRequestValidator : AbstractValidator<JwtTokenRequest>
    {
        public JwtTokenRequestValidator()
        {

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("You must fill the UserId")
                .GreaterThan(0).WithMessage("UserId must be a positive number");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("You must fill the FirstName")
                .MinimumLength(2).WithMessage("FirstName must be at least 2 characters")
                .MaximumLength(50).WithMessage("FirstName must be at most 50 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("You must fill the LastName")
                .MinimumLength(2).WithMessage("LastName must be at least 2 characters")
                .MaximumLength(50).WithMessage("LastName must be at most 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("You must fill the Email")
                .EmailAddress().WithMessage("Email must be a valid email address")
                .MaximumLength(255).WithMessage("Email must be at most 255 characters");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("You must fill the Phone")
                .Matches(@"^\+?[0-9]{8,15}$").WithMessage("Phone must be 8-15 digits, optionally starting with +");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("You must fill the Password")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .MaximumLength(50).WithMessage("Password must be at most 50 characters");
        }
    }
}
