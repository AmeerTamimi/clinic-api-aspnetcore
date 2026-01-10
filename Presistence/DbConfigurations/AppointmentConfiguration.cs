using ClinicAPI.Models;
using ClinicAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicAPI.Presistence.DbConfigurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");

            builder.HasKey(a => a.AppointmentId);

            builder.HasOne(a => a.Patient)
                   .WithMany(p => p.PatientAppointments)
                   .HasForeignKey(a => a.PatientUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Doctor)
                   .WithMany(d => d.DoctorAppointments)
                   .HasForeignKey(a => a.DoctorUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(a => a.PatientUserId)
                   .IsRequired();

            builder.Property(a => a.DoctorUserId)
                   .IsRequired();

            builder.Property(a => a.Symptoms)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(a => a.Diagnostic)
                   .HasMaxLength(500);

            builder.Property(a => a.Medicine)
                   .HasMaxLength(500);

            builder.Property(a => a.Date)
                   .IsRequired();

            builder.Property(a => a.IsDone)
                   .HasDefaultValue(false);

            builder.Property(a => a.CreatedAt)
                   .IsRequired();

            builder.Property(a => a.IsDeleted)
                   .HasDefaultValue(false);

            var baseCreatedAt = new DateTimeOffset(2025, 12, 14, 9, 0, 0, TimeSpan.Zero);
            var date = new DateTimeOffset(2025, 12, 18, 9, 0, 0, TimeSpan.FromHours(2));

            builder.HasData(
            [
                new Appointment { AppointmentId = 1,  DoctorUserId = 7, PatientUserId = 11,  Date = date, Symptoms = "Headache",       IsDone = false, CreatedAt = baseCreatedAt.AddDays(0), IsDeleted = false },
                new Appointment { AppointmentId = 2,  DoctorUserId = 2, PatientUserId = 12,  Date = date, Symptoms = "Skin rash",      Diagnostic = "Dermatitis", Medicine = "Cream", IsDone = true,  CreatedAt = baseCreatedAt.AddDays(0), IsDeleted = false },
                new Appointment { AppointmentId = 3,  DoctorUserId = 3, PatientUserId = 13,  Date = date, Symptoms = "Knee pain",      IsDone = false, CreatedAt = baseCreatedAt.AddDays(1), IsDeleted = false },
                new Appointment { AppointmentId = 4,  DoctorUserId = 5, PatientUserId = 14,  Date = date, Symptoms = "Sore throat",    IsDone = false, CreatedAt = baseCreatedAt.AddDays(1), IsDeleted = false },
                new Appointment { AppointmentId = 5,  DoctorUserId = 1, PatientUserId = 15,  Date = date, Symptoms = "Chest pain",     IsDone = false, CreatedAt = baseCreatedAt.AddDays(2), IsDeleted = false },
                new Appointment { AppointmentId = 6,  DoctorUserId = 8, PatientUserId = 16,  Date = date, Symptoms = "Blurred vision", Diagnostic = "Dry eye", Medicine = "Eye drops", IsDone = true,  CreatedAt = baseCreatedAt.AddDays(2), IsDeleted = false },
                new Appointment { AppointmentId = 7,  DoctorUserId = 4, PatientUserId = 17,  Date = date, Symptoms = "Fever",          IsDone = false, CreatedAt = baseCreatedAt.AddDays(3), IsDeleted = false },
                new Appointment { AppointmentId = 8,  DoctorUserId = 3, PatientUserId = 18,  Date = date, Symptoms = "Back pain",      IsDone = false, CreatedAt = baseCreatedAt.AddDays(3), IsDeleted = false },
                new Appointment { AppointmentId = 9,  DoctorUserId = 7, PatientUserId = 19,  Date = date, Symptoms = "Dizziness",      IsDone = false, CreatedAt = baseCreatedAt.AddDays(4), IsDeleted = false },
                new Appointment { AppointmentId = 10, DoctorUserId = 6, PatientUserId = 20, Date = date, Symptoms = "Migraine",       IsDone = false, CreatedAt = baseCreatedAt.AddDays(4), IsDeleted = false },
            ]);
        }
    }
}