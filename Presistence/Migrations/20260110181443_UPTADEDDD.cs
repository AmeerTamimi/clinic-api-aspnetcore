using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicAPI.Migrations
{
    /// <inheritdoc />
    public partial class UPTADEDDD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Salary",
                value: 12000m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Salary",
                value: 9500m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 3,
                column: "Salary",
                value: 11000m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 4,
                column: "Salary",
                value: 9000m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 5,
                column: "Salary",
                value: 13000m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 6,
                column: "Salary",
                value: 12500m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 7,
                column: "Salary",
                value: 10500m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 8,
                column: "Salary",
                value: 8000m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 9,
                column: "Salary",
                value: 11500m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 10,
                column: "Salary",
                value: 8800m);

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 11,
                columns: new[] { "Allergies", "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { "Penicillin", 1, 1, "Asthma history", 1 });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 12,
                columns: new[] { "Allergies", "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { "Peanuts", 2, 1, "High BP monitoring", 2 });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 13,
                columns: new[] { "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { 1, 2, "Routine follow-up", 0 });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 14,
                columns: new[] { "Allergies", "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { "Dust", 3, 3, "Joint pain", 2 });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 15,
                columns: new[] { "Allergies", "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { "Latex", 2, 4, "Pediatric consult", 1 });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 16,
                columns: new[] { "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { 0, 5, "ENT check", 1 });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 17,
                columns: new[] { "Allergies", "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { "Seafood", 1, 6, "Neurology follow-up", 2 });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 18,
                columns: new[] { "Allergies", "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { "Aspirin", 3, 7, "Cardio check", 2 });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 19,
                columns: new[] { "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { 2, 8, "General visit", 0 });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 20,
                columns: new[] { "Allergies", "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { "Pollen", 1, 9, "Ortho follow-up", 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "DoctorId10");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "DoctorId2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "PasswordHash",
                value: "DoctorId3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "PasswordHash",
                value: "DoctorId4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                column: "PasswordHash",
                value: "DoctorId5");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6,
                column: "PasswordHash",
                value: "DoctorId6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7,
                column: "PasswordHash",
                value: "DoctorId7");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8,
                column: "PasswordHash",
                value: "DoctorId8");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9,
                column: "PasswordHash",
                value: "DoctorId9");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10,
                column: "PasswordHash",
                value: "DoctorId10");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 11,
                columns: new[] { "Age", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Permissions", "Phone" },
                values: new object[] { 26, new DateTimeOffset(new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "khaled.abusaleh@clinic.test", "Khaled", "AbuSaleh", "PatientId11", "[\"patient:read\",\"patient:update\",\"patient:read-appointments\"]", "+970599000011" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 12,
                columns: new[] { "Age", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { 31, new DateTimeOffset(new DateTime(2025, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "rami.hassan@clinic.test", "Rami", "Hassan", "PatientId12", "+970599000012" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 13,
                columns: new[] { "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "huda.yasin@clinic.test", "Huda", "Yasin", "PatientId13", "+970599000013" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 14,
                columns: new[] { "Age", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { 44, new DateTimeOffset(new DateTime(2025, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "sami.naji@clinic.test", "Sami", "Naji", "PatientId14", "+970599000014" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 15,
                columns: new[] { "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "mona.sawalha@clinic.test", "Mona", "Sawalha", "PatientId15", "+970599000015" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 16,
                columns: new[] { "Age", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { 35, new DateTimeOffset(new DateTime(2025, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "tamer.karmi@clinic.test", "Tamer", "Karmi", "PatientId16", "+970599000016" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 17,
                columns: new[] { "Age", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { 19, new DateTimeOffset(new DateTime(2025, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "aya.mansour@clinic.test", "Aya", "Mansour", "PatientId17", "+970599000017" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 18,
                columns: new[] { "Age", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { 52, new DateTimeOffset(new DateTime(2025, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "bilal.hamdan@clinic.test", "Bilal", "Hamdan", "PatientId18", "+970599000018" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 19,
                columns: new[] { "Age", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { 24, new DateTimeOffset(new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "noor.zahran@clinic.test", "Noor", "Zahran", "PatientId19", "+970599000019" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 20,
                columns: new[] { "Age", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { 30, new DateTimeOffset(new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "yara.jabari@clinic.test", "Yara", "Jabari", "PatientId20", "+970599000020" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Salary",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Salary",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 3,
                column: "Salary",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 4,
                column: "Salary",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 5,
                column: "Salary",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 6,
                column: "Salary",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 7,
                column: "Salary",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 8,
                column: "Salary",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 9,
                column: "Salary",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 10,
                column: "Salary",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 11,
                columns: new[] { "Allergies", "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { null, null, 7, null, null });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 12,
                columns: new[] { "Allergies", "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { null, null, 2, null, null });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 13,
                columns: new[] { "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { null, 3, null, null });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 14,
                columns: new[] { "Allergies", "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { null, null, 5, null, null });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 15,
                columns: new[] { "Allergies", "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { null, null, 1, null, null });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 16,
                columns: new[] { "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { null, 8, null, null });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 17,
                columns: new[] { "Allergies", "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { null, null, 4, null, null });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 18,
                columns: new[] { "Allergies", "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { null, null, 3, null, null });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 19,
                columns: new[] { "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { null, 7, null, null });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 20,
                columns: new[] { "Allergies", "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[] { null, null, 6, null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "HASH");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "HASH");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "PasswordHash",
                value: "HASH");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "PasswordHash",
                value: "HASH");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                column: "PasswordHash",
                value: "HASH");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6,
                column: "PasswordHash",
                value: "HASH");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7,
                column: "PasswordHash",
                value: "HASH");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8,
                column: "PasswordHash",
                value: "HASH");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9,
                column: "PasswordHash",
                value: "HASH");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10,
                column: "PasswordHash",
                value: "HASH");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 11,
                columns: new[] { "Age", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Permissions", "Phone" },
                values: new object[] { 20, new DateTimeOffset(new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ameer.tamimi@clinic.test", "Ameer", "Tamimi", "HASH", "[\"patient:read\",\"patient:create\",\"patient:update\",\"patient:delete\",\"patient:read-appointments\",\"patient:manage-doctor\",\"patient:manage-appointments\",\"doctor:read\",\"doctor:create\",\"doctor:update\",\"doctor:delete\",\"doctor:read-patients\",\"doctor:read-appointments\",\"doctor:delete-patient\",\"doctor:add-patient\",\"doctor:update-specialty\",\"appointment:read\",\"appointment:create\",\"appointment:update\",\"appointment:delete\",\"appointment:update-status\",\"appointment:manage-patient\",\"appointment:manage-doctor\",\"appointment:update-date\"]", "+970599001001" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 12,
                columns: new[] { "Age", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { 21, new DateTimeOffset(new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "hareth.shoman@clinic.test", "Hareth", "Shoman", "HASH", "+970599001002" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 13,
                columns: new[] { "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "elyas.najeh@clinic.test", "Elyas", "Najeh", "HASH", "+970599001003" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 14,
                columns: new[] { "Age", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { 30, new DateTimeOffset(new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "mariam.yasin@clinic.test", "Mariam", "Yasin", "HASH", "+970599001004" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 15,
                columns: new[] { "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "kareem.abulail@clinic.test", "Kareem", "AbuLail", "HASH", "+970599001005" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 16,
                columns: new[] { "Age", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { 19, new DateTimeOffset(new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "noor.said@clinic.test", "Noor", "Said", "HASH", "+970599001006" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 17,
                columns: new[] { "Age", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { 23, new DateTimeOffset(new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "zaid.qamhieh@clinic.test", "Zaid", "Qamhieh", "HASH", "+970599001007" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 18,
                columns: new[] { "Age", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { 24, new DateTimeOffset(new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "habeeb.ahmad@clinic.test", "Habeeb", "Ahmad", "HASH", "+970599001008" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 19,
                columns: new[] { "Age", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { 26, new DateTimeOffset(new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "waleed.noubani@clinic.test", "Waleed", "Noubani", "HASH", "+970599001009" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 20,
                columns: new[] { "Age", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[] { 29, new DateTimeOffset(new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ruba.katout@clinic.test", "Ruba", "Katout", "HASH", "+970599001010" });
        }
    }
}
