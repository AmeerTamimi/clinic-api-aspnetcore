using ClinicAPI.Configurations;
using ClinicAPI.CustomExceptions;
using ClinicAPI.Permissions;
using ClinicAPI.Presistence;
using ClinicAPI.Repositories;
using ClinicAPI.Service;
using ClinicAPI.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace ClinicAPI.Registirations
{
    public static class ApplicationCollections
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services , IConfiguration config)
        {
            services.AddBusinessServices()
                    .AddJwtAuthentication()
                    .AddJwtAuthorization()
                    .AddInfrastructureServices()
                    .AddProblemDetails()
                    .AddDb()
                    .AddExceptionHandler<GlobalExceptionHandler>()
                    .AddDocumentation()
                    .AddValidatorsServices()
                    .AddConfigurations(config);
                 



            return services;
        }
        private static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            return services;
        }

        private static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            services.ConfigureOptions<JwtBearerConfigurations>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer();

            return services;
        }

        private static IServiceCollection AddJwtAuthorization(this IServiceCollection services)
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

        private static IServiceCollection AddValidatorsServices(this IServiceCollection services)
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

        private static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentRepo, AppointmentRepo>();
            services.AddScoped<IDoctorRepo, DoctorRepo>();
            services.AddScoped<IPatientRepo, PatientRepo>();
            services.AddScoped<IRefreshTokenRepo, RefreshTokenRepo>();
            services.AddScoped<UserRepo>();

            return services;
        }
        private static IServiceCollection AddDb(this IServiceCollection services)
        {
            services.AddDbContext<ClinicDbContext>(options =>
            {
                options.UseSqlite("Data Source = clinic.db");
            }).AddDataProtection()
            .PersistKeysToDbContext<ClinicDbContext>();

            return services;
        }

        private static IServiceCollection AddDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer()
                    .AddSwaggerGen();

            return services;
        }
        private static IServiceCollection AddConfigurations(this IServiceCollection services , IConfiguration config)
        {
            services.AddOptions<ClinicSettings>()
                    .Bind(config.GetSection(ClinicSettings.clinicName));

            services.AddOptions<JwtSettings>()
                    .Bind(config.GetSection(JwtSettings.JwtSettingsName));

            services.AddControllers()
                    .AddJsonOptions(o =>
                                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                                    );
            return services;
        }
    }
}
