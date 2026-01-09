using ClinicAPI.Enums;
using ClinicAPI.Models;
using ClinicAPI.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using System.Text.Json;

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

            builder.Property(d => d.Email)
                   .IsRequired()
                   .HasMaxLength(120);

            builder.HasIndex(d => d.Email)
                   .IsUnique();

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

            builder.Property(d => d.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(200);

           
            builder.Property(d => d.CreatedAt)
                   .IsRequired();

            builder.Property(d => d.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasData(
            [
                new Doctor
                {
                    DoctorId = 1,
                    FirstName = "Ahmad",
                    LastName = "Khalil",
                    Email = "ahmad.khalil@clinic.test",
                    Age = 40,
                    Phone = "+970599000001",
                    PasswordHash = "HASH",
                    Specialty = DoctorSpecialty.Cardiology,
                    YearOfExperience = 15,
                    Permissions =
                    [
                        Permission.Patient.Read,
                        Permission.Patient.Create,
                        Permission.Patient.Update,
                        Permission.Patient.Delete,
                        Permission.Patient.ReadAppointments,
                        Permission.Patient.ManageDoctor,
                        Permission.Patient.ManageAppointments,

                        Permission.Doctor.Read,
                        Permission.Doctor.Create,
                        Permission.Doctor.Update,
                        Permission.Doctor.Delete,
                        Permission.Doctor.ReadPatients,
                        Permission.Doctor.ReadAppointments,
                        Permission.Doctor.DeletePatient,
                        Permission.Doctor.AddPatient,
                        Permission.Doctor.UpdateSpecialty,

                        Permission.Appointment.Read,
                        Permission.Appointment.Create,
                        Permission.Appointment.Update,
                        Permission.Appointment.Delete,
                        Permission.Appointment.UpdateStatus,
                        Permission.Appointment.ManagePatient,
                        Permission.Appointment.ManageDoctor,
                        Permission.Appointment.UpdateDate
                    ],
                    Roles = ["Doctor"],
                    CreatedAt = new DateTimeOffset(2025, 10, 28, 0, 0, 0, TimeSpan.Zero),
                    IsDeleted = false
                },
                new Doctor { DoctorId = 2,  FirstName = "Rana",   LastName = "Haddad",    Email = "rana.haddad@clinic.test",   Age = 34, Phone = "+970599000002", PasswordHash = "HASH", Specialty = DoctorSpecialty.Dermatology,     YearOfExperience = 10, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 11,  2, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Doctor { DoctorId = 3,  FirstName = "Omar",   LastName = "Safi",      Email = "omar.safi@clinic.test",     Age = 38, Phone = "+970599000003", PasswordHash = "HASH", Specialty = DoctorSpecialty.Orthopedics,     YearOfExperience = 12, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 11,  7, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Doctor { DoctorId = 4,  FirstName = "Lina",   LastName = "Barghouti", Email = "lina.barghouti@clinic.test",Age = 33, Phone = "+970599000004", PasswordHash = "HASH", Specialty = DoctorSpecialty.Pediatrics,      YearOfExperience =  8, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 11, 12, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Doctor { DoctorId = 5,  FirstName = "Yousef", LastName = "Nassar",    Email = "yousef.nassar@clinic.test", Age = 45, Phone = "+970599000005", PasswordHash = "HASH", Specialty = DoctorSpecialty.ENT,             YearOfExperience = 20, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 11, 17, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Doctor { DoctorId = 6,  FirstName = "Hala",   LastName = "Masri",     Email = "hala.masri@clinic.test",    Age = 41, Phone = "+970599000006", PasswordHash = "HASH", Specialty = DoctorSpecialty.Neurology,       YearOfExperience = 16, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 11, 22, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Doctor { DoctorId = 7,  FirstName = "Tariq",  LastName = "Qasem",     Email = "tariq.qasem@clinic.test",   Age = 36, Phone = "+970599000007", PasswordHash = "HASH", Specialty = DoctorSpecialty.Cardiology,      YearOfExperience = 11, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 11, 27, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Doctor { DoctorId = 8,  FirstName = "Maya",   LastName = "Ayyad",     Email = "maya.ayyad@clinic.test",    Age = 29, Phone = "+970599000008", PasswordHash = "HASH", Specialty = DoctorSpecialty.GeneralPractice, YearOfExperience =  5, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 12,  2, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Doctor { DoctorId = 9,  FirstName = "Nabil",  LastName = "Salameh",   Email = "nabil.salameh@clinic.test", Age = 39, Phone = "+970599000009", PasswordHash = "HASH", Specialty = DoctorSpecialty.Neurology,       YearOfExperience = 13, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 12,  7, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Doctor { DoctorId = 10, FirstName = "Sara",   LastName = "Zaid",      Email = "sara.zaid@clinic.test",     Age = 32, Phone = "+970599000010", PasswordHash = "HASH", Specialty = DoctorSpecialty.Orthopedics,     YearOfExperience =  7, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 12, 12, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
            ]);
        }
    }
}
