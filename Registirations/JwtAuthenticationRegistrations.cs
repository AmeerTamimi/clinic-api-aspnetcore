using ClinicAPI.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ClinicAPI.Registirations
{
    public static class JwtAuthenticationRegistrations
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            services.ConfigureOptions<JwtBearerConfigurations>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer();

            return services;
        }
    }
}
