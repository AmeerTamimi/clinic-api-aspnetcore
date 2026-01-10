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

            //builder.HasKey(d => d.UserId);

            builder.Property(d => d.Specialty)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(30);

            builder.Property(d => d.YearOfExperience)
                   .IsRequired();

            List<User> doctors = new()
            {
                new Doctor
                {
                    UserId = 1,
                    Type = UserType.Doctor ,
                    FirstName = "Ahmad",
                    LastName = "Khalil",
                    Email = "ahmad.khalil@clinic.test",
                    Age = 40,
                    Phone = "+970599000001",
                    PasswordHash = "DoctorId10",

                    Salary = 12000m,

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

                new Doctor { UserId = 2, Type = UserType.Doctor, FirstName = "Rana",  LastName = "Haddad",    Email = "rana.haddad@clinic.test",    Age = 34, Phone = "+970599000002", PasswordHash = "DoctorId2", Salary = 9500m,  Specialty = DoctorSpecialty.Dermatology,     YearOfExperience = 10, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 11,  2, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Doctor { UserId = 3, Type = UserType.Doctor, FirstName = "Omar",  LastName = "Safi",      Email = "omar.safi@clinic.test",      Age = 38, Phone = "+970599000003", PasswordHash = "DoctorId3", Salary = 11000m, Specialty = DoctorSpecialty.Orthopedics,     YearOfExperience = 12, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 11,  7, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Doctor { UserId = 4, Type = UserType.Doctor, FirstName = "Lina",  LastName = "Barghouti", Email = "lina.barghouti@clinic.test", Age = 33, Phone = "+970599000004", PasswordHash = "DoctorId4", Salary = 9000m,  Specialty = DoctorSpecialty.Pediatrics,      YearOfExperience =  8, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 11, 12, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Doctor { UserId = 5, Type = UserType.Doctor, FirstName = "Yousef",LastName = "Nassar",    Email = "yousef.nassar@clinic.test",  Age = 45, Phone = "+970599000005", PasswordHash = "DoctorId5", Salary = 13000m, Specialty = DoctorSpecialty.ENT,             YearOfExperience = 20, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 11, 17, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Doctor { UserId = 6, Type = UserType.Doctor, FirstName = "Hala",  LastName = "Masri",     Email = "hala.masri@clinic.test",     Age = 41, Phone = "+970599000006", PasswordHash = "DoctorId6", Salary = 12500m, Specialty = DoctorSpecialty.Neurology,       YearOfExperience = 16, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 11, 22, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Doctor { UserId = 7, Type = UserType.Doctor, FirstName = "Tariq", LastName = "Qasem",     Email = "tariq.qasem@clinic.test",    Age = 36, Phone = "+970599000007", PasswordHash = "DoctorId7", Salary = 10500m, Specialty = DoctorSpecialty.Cardiology,      YearOfExperience = 11, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 11, 27, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Doctor { UserId = 8, Type = UserType.Doctor, FirstName = "Maya",  LastName = "Ayyad",     Email = "maya.ayyad@clinic.test",     Age = 29, Phone = "+970599000008", PasswordHash = "DoctorId8", Salary = 8000m,  Specialty = DoctorSpecialty.GeneralPractice, YearOfExperience =  5, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 12,  2, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Doctor { UserId = 9, Type = UserType.Doctor, FirstName = "Nabil", LastName = "Salameh",   Email = "nabil.salameh@clinic.test",  Age = 39, Phone = "+970599000009", PasswordHash = "DoctorId9", Salary = 11500m, Specialty = DoctorSpecialty.Neurology,       YearOfExperience = 13, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 12,  7, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
                new Doctor { UserId = 10,Type = UserType.Doctor, FirstName = "Sara",  LastName = "Zaid",      Email = "sara.zaid@clinic.test",      Age = 32, Phone = "+970599000010", PasswordHash = "DoctorId10", Salary = 8800m,  Specialty = DoctorSpecialty.Orthopedics,     YearOfExperience =  7, Permissions = [], Roles = ["Doctor"], CreatedAt = new DateTimeOffset(2025, 12, 12, 0, 0, 0, TimeSpan.Zero), IsDeleted = false },
            };

            builder.HasData(doctors);
        }
    }
}
