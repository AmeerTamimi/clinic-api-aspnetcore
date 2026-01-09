using ClinicAPI.Permissions;

namespace ClinicAPI.Registirations
{
    public static class JwtAuthorizationRegistirations
    {
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Permission.Patient.Read, policy => policy.RequireClaim(Permission.ClaimType, Permission.Patient.Read));
                options.AddPolicy(Permission.Patient.Create, policy => policy.RequireClaim(Permission.ClaimType, Permission.Patient.Create));
                options.AddPolicy(Permission.Patient.Update, policy => policy.RequireClaim(Permission.ClaimType, Permission.Patient.Update));
                options.AddPolicy(Permission.Patient.Delete, policy => policy.RequireClaim(Permission.ClaimType, Permission.Patient.Delete));
                options.AddPolicy(Permission.Patient.ReadAppointments, policy => policy.RequireClaim(Permission.ClaimType, Permission.Patient.ReadAppointments));
                options.AddPolicy(Permission.Patient.ManageDoctor, policy => policy.RequireClaim(Permission.ClaimType, Permission.Patient.ManageDoctor));
                options.AddPolicy(Permission.Patient.ManageAppointments, policy => policy.RequireClaim(Permission.ClaimType, Permission.Patient.ManageAppointments));

                options.AddPolicy(Permission.Doctor.Read, policy => policy.RequireClaim(Permission.ClaimType, Permission.Doctor.Read));
                options.AddPolicy(Permission.Doctor.Create, policy => policy.RequireClaim(Permission.ClaimType, Permission.Doctor.Create));
                options.AddPolicy(Permission.Doctor.Update, policy => policy.RequireClaim(Permission.ClaimType, Permission.Doctor.Update));
                options.AddPolicy(Permission.Doctor.Delete, policy => policy.RequireClaim(Permission.ClaimType, Permission.Doctor.Delete));
                options.AddPolicy(Permission.Doctor.ReadPatients, policy => policy.RequireClaim(Permission.ClaimType, Permission.Doctor.ReadPatients));
                options.AddPolicy(Permission.Doctor.ReadAppointments, policy => policy.RequireClaim(Permission.ClaimType, Permission.Doctor.ReadAppointments));
                options.AddPolicy(Permission.Doctor.DeletePatient, policy => policy.RequireClaim(Permission.ClaimType, Permission.Doctor.DeletePatient));
                options.AddPolicy(Permission.Doctor.AddPatient, policy => policy.RequireClaim(Permission.ClaimType, Permission.Doctor.AddPatient));
                options.AddPolicy(Permission.Doctor.UpdateSpecialty, policy => policy.RequireClaim(Permission.ClaimType, Permission.Doctor.UpdateSpecialty));


                options.AddPolicy(Permission.Appointment.Read, policy => policy.RequireClaim(Permission.ClaimType, Permission.Appointment.Read));
                options.AddPolicy(Permission.Appointment.Create, policy => policy.RequireClaim(Permission.ClaimType, Permission.Appointment.Create));
                options.AddPolicy(Permission.Appointment.Update, policy => policy.RequireClaim(Permission.ClaimType, Permission.Appointment.Update));
                options.AddPolicy(Permission.Appointment.Delete, policy => policy.RequireClaim(Permission.ClaimType, Permission.Appointment.Delete));
                options.AddPolicy(Permission.Appointment.UpdateStatus, policy => policy.RequireClaim(Permission.ClaimType, Permission.Appointment.UpdateStatus));
                options.AddPolicy(Permission.Appointment.ManagePatient, policy => policy.RequireClaim(Permission.ClaimType, Permission.Appointment.ManagePatient));
                options.AddPolicy(Permission.Appointment.ManageDoctor, policy => policy.RequireClaim(Permission.ClaimType, Permission.Appointment.ManageDoctor));
                options.AddPolicy(Permission.Appointment.UpdateDate, policy => policy.RequireClaim(Permission.ClaimType, Permission.Appointment.UpdateDate));
            });

            return services;
        }
    }
}
