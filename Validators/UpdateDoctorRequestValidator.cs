using ClinicAPI.Requests;
using FluentValidation;

namespace ClinicAPI.Validators
{
    public class UpdateDoctorRequestValidator : AbstractValidator<UpdateDoctorRequest>
    {
        public UpdateDoctorRequestValidator()
        {
            RuleFor(d => d.FirstName)
            .NotEmpty().WithMessage("First Name is required")
            .MaximumLength(255).WithMessage("First Name must be <= 255 characters")
            .Matches("^[A-Za-z]+$").WithMessage("First Name Must Contain Only Letters");

            RuleFor(d => d.LastName)
                .NotEmpty().WithMessage("Last Name is required")
                .MaximumLength(255).WithMessage("Last Name must be <= 255 characters")
                .Matches("^[A-Za-z]+$").WithMessage("Last Name Must Contain Only Letters");

            RuleFor(d => d.Specialty)
                .IsInEnum().WithMessage("Invalid Specialty");

            RuleFor(d => d.Phone)
                .NotEmpty().WithMessage("Phone is required")
                .Matches(@"^[0-9]{10,12}$").WithMessage("Invalid Number Format");

            RuleFor(d => d.Age)
                .InclusiveBetween(18, 100).WithMessage("Age must be between 18 and 100");

            RuleFor(d => d.YearOfExperience)
                .InclusiveBetween(0, 60).WithMessage("Year Of Experience must be between 0 and 60")
                .LessThanOrEqualTo(d => d.Age - 18)
                .WithMessage("Year Of Experience can't be more than (Age - 18)");
        }
    }
}
