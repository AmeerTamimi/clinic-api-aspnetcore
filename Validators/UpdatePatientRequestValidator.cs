using ClinicAPI.Requests;
using FluentValidation;

namespace ClinicAPI.Validators
{
    public class UpdatePatientRequestValidator : UserRequestValidator<UpdatePatientRequest>
    {
        public UpdatePatientRequestValidator()
        {
            RuleFor(p => p.DoctorId)
                .NotEmpty().WithMessage("You must fill the DoctorId")
                .GreaterThan(0).WithMessage("DoctorId must be a positive number");

            RuleFor(p => p.RiskLevel)
                .IsInEnum().WithMessage("Invalid RiskLevel");

            RuleFor(p => p.BloodType)
                .IsInEnum().WithMessage("Invalid BloodType");

            RuleFor(p => p.Allergies)
                .MaximumLength(300).WithMessage("Allergies must be at most 300 characters");

            RuleFor(p => p.Note)
                .MaximumLength(500).WithMessage("Note must be at most 500 characters");
        }
    }
}

