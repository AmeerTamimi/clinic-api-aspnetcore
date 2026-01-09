using ClinicAPI.Service;

namespace ClinicAPI.Registirations
{
    public static class BusinessCollections
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services) 
        {
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}
