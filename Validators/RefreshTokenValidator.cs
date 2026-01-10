using ClinicAPI.Requests;
using FluentValidation;

namespace ClinicAPI.Validators
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenValidator()
        {
            RuleFor(r => r.RefreshToken)
                .NotEmpty().WithMessage("No Refresh Token Sent");
        }
    }
}
