using ClinicAPI.Models;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicAPI.Presistence
{
    public class ClinicDbContext(DbContextOptions<ClinicDbContext> options) : DbContext(options)
        , IDataProtectionKeyContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<RefreshTokenModel> RefreshTokens { get; set; }
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ClinicDbContext).Assembly);
        }
    }
}
