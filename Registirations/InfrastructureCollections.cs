using ClinicAPI.Repositories;

namespace ClinicAPI.Registirations
{
    public static class InfrastructureCollections
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentRepo, AppointmentRepo>();
            services.AddScoped<IDoctorRepo, DoctorRepo>();
            services.AddScoped<IPatientRepo, PatientRepo>();
            services.AddScoped<IRefreshTokenRepo, RefreshTokenRepo>();
            services.AddScoped<UserRepo>();

            return services;
        }
    }
}
