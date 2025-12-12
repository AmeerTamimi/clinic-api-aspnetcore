using ClinicAPI.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicAPI.GroupedRegistirations
{
    public static class InfrastructureCollections
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentRepo, AppointmentRepo>();
            services.AddScoped<IDoctorRepo, DoctorRepo>();
            services.AddScoped<IPatientRepo, PatientRepo>();

            return services;
        }
    }
}
