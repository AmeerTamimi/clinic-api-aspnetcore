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
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            //builder.HasKey(p => p.UserId);

            builder.HasOne(p => p.Doctor)
                   .WithMany(d => d.DoctorPatients)
                   .HasForeignKey(p => p.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            
            builder.Property(p => p.DoctorId)
                   .IsRequired();



            List<User> patients = new()
            {
                new Patient
                {
                    UserId = 11,
                    Type = UserType.Patient,
                    FirstName = "Khaled",
                    LastName = "AbuSaleh",
                    Email = "khaled.abusaleh@clinic.test",
                    Age = 26,
                    Phone = "+970599000011",
                    PasswordHash = "PatientId11",

                    DoctorId = 1,

                    Allergies = "Penicillin",
                    BloodType = (BloodType?)1,
                    RiskLevel = (RiskLevel?)1,
                    Note = "Asthma history",

                    Permissions =
                    [
                        Permission.Patient.Read,
                        Permission.Patient.Update,
                        Permission.Patient.ReadAppointments
                    ],
                    Roles = ["Patient"],
                    CreatedAt = new DateTimeOffset(2025, 11, 1, 0, 0, 0, TimeSpan.Zero),
                    IsDeleted = false
                },

                new Patient {UserId = 12, Type = UserType.Patient, FirstName = "Rami",   LastName = "Hassan",   Email = "rami.hassan@clinic.test",   Age = 31, Phone = "+970599000012", PasswordHash = "PatientId12", DoctorId = 1, Allergies = "Peanuts",        BloodType = (BloodType?)2, RiskLevel = (RiskLevel?)2, Note = "High BP monitoring", Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 11, 3, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Patient {UserId = 13, Type = UserType.Patient, FirstName = "Huda",   LastName = "Yasin",    Email = "huda.yasin@clinic.test",    Age = 22, Phone = "+970599000013", PasswordHash = "PatientId13", DoctorId = 2, Allergies = null,             BloodType = (BloodType?)1, RiskLevel = (RiskLevel?)0, Note = "Routine follow-up",     Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 11, 5, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Patient {UserId = 14, Type = UserType.Patient, FirstName = "Sami",   LastName = "Naji",     Email = "sami.naji@clinic.test",     Age = 44, Phone = "+970599000014", PasswordHash = "PatientId14", DoctorId = 3, Allergies = "Dust",           BloodType = (BloodType?)3, RiskLevel = (RiskLevel?)2, Note = "Joint pain",           Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 11, 8, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Patient {UserId = 15, Type = UserType.Patient, FirstName = "Mona",   LastName = "Sawalha",  Email = "mona.sawalha@clinic.test",  Age = 28, Phone = "+970599000015", PasswordHash = "PatientId15", DoctorId = 4, Allergies = "Latex",          BloodType = (BloodType?)2, RiskLevel = (RiskLevel?)1, Note = "Pediatric consult",    Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 11,10, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Patient {UserId = 16, Type = UserType.Patient, FirstName = "Tamer",  LastName = "Karmi",    Email = "tamer.karmi@clinic.test",    Age = 35, Phone = "+970599000016", PasswordHash = "PatientId16", DoctorId = 5, Allergies = null,             BloodType = (BloodType?)0, RiskLevel = (RiskLevel?)1, Note = "ENT check",            Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 11,13, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Patient {UserId = 17, Type = UserType.Patient, FirstName = "Aya",    LastName = "Mansour",  Email = "aya.mansour@clinic.test",  Age = 19, Phone = "+970599000017", PasswordHash = "PatientId17", DoctorId = 6, Allergies = "Seafood",        BloodType = (BloodType?)1, RiskLevel = (RiskLevel?)2, Note = "Neurology follow-up",  Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 11,16, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Patient {UserId = 18, Type = UserType.Patient, FirstName = "Bilal",  LastName = "Hamdan",   Email = "bilal.hamdan@clinic.test",   Age = 52, Phone = "+970599000018", PasswordHash = "PatientId18", DoctorId = 7, Allergies = "Aspirin",        BloodType = (BloodType?)3, RiskLevel = (RiskLevel?)2, Note = "Cardio check",          Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 11,19, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Patient {UserId = 19, Type = UserType.Patient, FirstName = "Noor",   LastName = "Zahran",   Email = "noor.zahran@clinic.test",   Age = 24, Phone = "+970599000019", PasswordHash = "PatientId19", DoctorId = 8, Allergies = null,             BloodType = (BloodType?)2, RiskLevel = (RiskLevel?)0, Note = "General visit",         Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 11,22, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Patient {UserId = 20, Type = UserType.Patient, FirstName = "Yara",   LastName = "Jabari",   Email = "yara.jabari@clinic.test",   Age = 30, Phone = "+970599000020", PasswordHash = "PatientId20", DoctorId = 9, Allergies = "Pollen",         BloodType = (BloodType?)1, RiskLevel = (RiskLevel?)1, Note = "Ortho follow-up",       Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 11,25, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
            };

            builder.HasData(patients);
        }
    }
}
