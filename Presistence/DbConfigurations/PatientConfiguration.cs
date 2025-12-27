using ClinicAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicAPI.Presistence.DbConfigurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(p => p.PatientId);

            builder.HasOne(p => p.Doctor)
                   .WithMany(d => d.DoctorPatients)
                   .HasForeignKey(p => p.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.Age)
                   .IsRequired();

            builder.Property(p => p.DoctorId)
                   .IsRequired();

            builder.Property(p => p.CreatedAt)
                   .IsRequired();

            builder.Property(p => p.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasData(
            [
                new Patient { PatientId = 1,  FirstName = "Ameer",  LastName = "Tamimi",  Age = 20, DoctorId = 7, CreatedAt = new DateTime(2025, 12, 13, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Patient { PatientId = 2,  FirstName = "Hareth", LastName = "Shoman",  Age = 21, DoctorId = 2, CreatedAt = new DateTime(2025, 12, 14, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Patient { PatientId = 3,  FirstName = "Elyas",  LastName = "Najeh",   Age = 22, DoctorId = 3, CreatedAt = new DateTime(2025, 12, 15, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Patient { PatientId = 4,  FirstName = "Mariam", LastName = "Yasin",   Age = 30, DoctorId = 5, CreatedAt = new DateTime(2025, 12, 16, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Patient { PatientId = 5,  FirstName = "Kareem", LastName = "AbuLail", Age = 28, DoctorId = 1, CreatedAt = new DateTime(2025, 12, 17, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Patient { PatientId = 6,  FirstName = "Noor",   LastName = "Said",    Age = 19, DoctorId = 8, CreatedAt = new DateTime(2025, 12, 18, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Patient { PatientId = 7,  FirstName = "Zaid",   LastName = "Qamhieh", Age = 23, DoctorId = 4, CreatedAt = new DateTime(2025, 12, 19, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Patient { PatientId = 8,  FirstName = "Habeeb", LastName = "Ahmad",   Age = 24, DoctorId = 3, CreatedAt = new DateTime(2025, 12, 20, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Patient { PatientId = 9,  FirstName = "Waleed", LastName = "Noubani", Age = 26, DoctorId = 7, CreatedAt = new DateTime(2025, 12, 21, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Patient { PatientId = 10, FirstName = "Ruba",   LastName = "Katout",  Age = 29, DoctorId = 6, CreatedAt = new DateTime(2025, 12, 22, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false }
            ]);
        }
    }
}
