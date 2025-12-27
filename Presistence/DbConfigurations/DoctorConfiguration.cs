using ClinicAPI.Enums;
using ClinicAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicAPI.Presistence.DbConfigurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.HasKey(d => d.DoctorId);

            builder.Property(d => d.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(d => d.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(d => d.Specialty)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(30);

            builder.Property(d => d.Phone)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(d => d.Age)
                   .IsRequired();

            builder.Property(d => d.YearOfExperience)
                   .IsRequired();

            builder.Property(d => d.CreatedAt)
                   .IsRequired();

            builder.Property(d => d.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasData(
            [
                new Doctor { DoctorId = 1,  FirstName = "Ahmad",  LastName = "Khalil",    Specialty = DoctorSpecialty.Cardiology,       Phone = "+970599000001", CreatedAt = new DateTime(2025, 10, 28, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Doctor { DoctorId = 2,  FirstName = "Rana",   LastName = "Haddad",    Specialty = DoctorSpecialty.Dermatology,      Phone = "+970599000002", CreatedAt = new DateTime(2025, 11,  2, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Doctor { DoctorId = 3,  FirstName = "Omar",   LastName = "Safi",      Specialty = DoctorSpecialty.Orthopedics,      Phone = "+970599000003", CreatedAt = new DateTime(2025, 11,  7, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Doctor { DoctorId = 4,  FirstName = "Lina",   LastName = "Barghouti", Specialty = DoctorSpecialty.Pediatrics,       Phone = "+970599000004", CreatedAt = new DateTime(2025, 11, 12, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Doctor { DoctorId = 5,  FirstName = "Yousef", LastName = "Nassar",    Specialty = DoctorSpecialty.ENT,              Phone = "+970599000005", CreatedAt = new DateTime(2025, 11, 17, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Doctor { DoctorId = 6,  FirstName = "Hala",   LastName = "Masri",     Specialty = DoctorSpecialty.Neurology,        Phone = "+970599000006", CreatedAt = new DateTime(2025, 11, 22, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Doctor { DoctorId = 7,  FirstName = "Tariq",  LastName = "Qasem",     Specialty = DoctorSpecialty.Cardiology,       Phone = "+970599000007", CreatedAt = new DateTime(2025, 11, 27, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Doctor { DoctorId = 8,  FirstName = "Maya",   LastName = "Ayyad",     Specialty = DoctorSpecialty.GeneralPractice,  Phone = "+970599000008", CreatedAt = new DateTime(2025, 12,  2, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Doctor { DoctorId = 9,  FirstName = "Nabil",  LastName = "Salameh",   Specialty = DoctorSpecialty.Neurology,        Phone = "+970599000009", CreatedAt = new DateTime(2025, 12,  7, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Doctor { DoctorId = 10, FirstName = "Sara",   LastName = "Zaid",      Specialty = DoctorSpecialty.Orthopedics,      Phone = "+970599000010", CreatedAt = new DateTime(2025, 12, 12, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
            ]);

        }
    }
}
