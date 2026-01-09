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

            builder.Property(p => p.Email)
                   .HasMaxLength(120);

            builder.Property(p => p.Phone)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(p => p.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(200);

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
                new Patient
                    {
                        PatientId = 1,
                        FirstName = "Ameer",
                        LastName = "Tamimi",
                        Email = "ameer.tamimi@clinic.test",
                        Age = 20,
                        Phone = "+970599001001",
                        PasswordHash = "HASH",
                        DoctorId = 7,
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
                        Roles = ["Patient"],
                        CreatedAt = new DateTimeOffset(2025, 12, 13, 0, 0, 0, TimeSpan.Zero),
                        IsDeleted = false
                    },

                new Patient { PatientId = 2,  FirstName = "Hareth", LastName = "Shoman",  Email = "hareth.shoman@clinic.test", Age = 21, Phone = "+970599001002", PasswordHash = "HASH", DoctorId = 2, Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 12, 14, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Patient { PatientId = 3,  FirstName = "Elyas",  LastName = "Najeh",   Email = "elyas.najeh@clinic.test",   Age = 22, Phone = "+970599001003", PasswordHash = "HASH", DoctorId = 3, Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 12, 15, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Patient { PatientId = 4,  FirstName = "Mariam", LastName = "Yasin",   Email = "mariam.yasin@clinic.test",  Age = 30, Phone = "+970599001004", PasswordHash = "HASH", DoctorId = 5, Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 12, 16, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Patient { PatientId = 5,  FirstName = "Kareem", LastName = "AbuLail", Email = "kareem.abulail@clinic.test",Age = 28, Phone = "+970599001005", PasswordHash = "HASH", DoctorId = 1, Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 12, 17, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Patient { PatientId = 6,  FirstName = "Noor",   LastName = "Said",    Email = "noor.said@clinic.test",     Age = 19, Phone = "+970599001006", PasswordHash = "HASH", DoctorId = 8, Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 12, 18, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Patient { PatientId = 7,  FirstName = "Zaid",   LastName = "Qamhieh", Email = "zaid.qamhieh@clinic.test",  Age = 23, Phone = "+970599001007", PasswordHash = "HASH", DoctorId = 4, Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 12, 19, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Patient { PatientId = 8,  FirstName = "Habeeb", LastName = "Ahmad",   Email = "habeeb.ahmad@clinic.test",  Age = 24, Phone = "+970599001008", PasswordHash = "HASH", DoctorId = 3, Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 12, 20, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Patient { PatientId = 9,  FirstName = "Waleed", LastName = "Noubani", Email = "waleed.noubani@clinic.test", Age = 26, Phone = "+970599001009", PasswordHash = "HASH", DoctorId = 7, Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 12, 21, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Patient { PatientId = 10, FirstName = "Ruba",   LastName = "Katout",  Email = "ruba.katout@clinic.test",   Age = 29, Phone = "+970599001010", PasswordHash = "HASH", DoctorId = 6, Permissions = [], Roles = ["Patient"], CreatedAt = new DateTimeOffset(2025, 12, 22, 0, 0, 0, TimeSpan.Zero), IsDeleted = false }
            ]);
        }
    }
}
