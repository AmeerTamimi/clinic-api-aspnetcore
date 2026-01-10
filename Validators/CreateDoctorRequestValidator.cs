using ClinicAPI.Requests;
using FluentValidation;

namespace ClinicAPI.Validators
{
    public class CreateDoctorRequestValidator : UserRequestValidator<CreateDoctorRequest>
    {
        public CreateDoctorRequestValidator()
        {

            RuleFor(d => d.Specialty)
                .IsInEnum().WithMessage("Invalid Specialty");

            RuleFor(d => d.YearOfExperience)
                .InclusiveBetween(0, 60).WithMessage("Year Of Experience must be between 0 and 60")
                .LessThanOrEqualTo(d => d.Age - 18)
                .WithMessage("Year Of Experience can't be more than (Age - 18)");

            RuleFor(d => d.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0")
                .LessThanOrEqualTo(1_000_000m).WithMessage("Salary is too high");
        }
    }
}
