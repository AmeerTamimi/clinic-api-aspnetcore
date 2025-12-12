using ClinicAPI.Service;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicAPI.GroupedRegistirations
{
    public static class BusinessCollections
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services) 
        {
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IDoctorService, DoctorService>();

            return services;
        }
    }
}
