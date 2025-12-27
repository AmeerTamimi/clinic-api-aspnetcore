using ClinicAPI.Requests;
using FluentValidation;

namespace ClinicAPI.Validators
{
    public class UpdateAppointmentRequestValidator : AbstractValidator<UpdateAppointmentRequest>
    {
        public UpdateAppointmentRequestValidator()
        {
            RuleFor(a => a.PatientId)
            .GreaterThan(0).WithMessage("PatientId must be a positive number");

            RuleFor(a => a.DoctorId)
                .GreaterThan(0).WithMessage("DoctorId must be a positive number");

            RuleFor(a => a.Symptoms)
                .NotEmpty().WithMessage("Symptoms is required");

            RuleFor(a => a.Date)
                .NotEmpty().WithMessage("Appointment Date is required")
                .Must(TodayOrFuture).WithMessage("Appointment Date can't be in the past");
        }


        private bool TodayOrFuture(DateTimeOffset date)
        {
            return date.UtcDateTime.Date >= DateTime.UtcNow.Date;
        }
    }
}
