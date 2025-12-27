using ClinicAPI.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace ClinicAPI.GroupedRegistirations
{
    public static class ValidatorsCollection
    {
        public static IServiceCollection AddValidatorsServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining<CreatePatientRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateDoctorRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateAppointmentRequestValidator>();

            services.AddValidatorsFromAssemblyContaining<UpdatePatientRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateDoctorRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateAppointmentRequestValidator>();

            return services;
        }
    }
}
