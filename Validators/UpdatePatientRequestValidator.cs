using ClinicAPI.Requests;
using FluentValidation;

public class UpdatePatientRequestValidator : AbstractValidator<UpdatePatientRequest>
{
    public UpdatePatientRequestValidator()
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

        RuleFor(p => p.DoctorId)
            .NotEmpty().WithMessage("You must fill the DoctorId")
            .GreaterThan(0).WithMessage("DoctorId must be a positive number");
    }
}
