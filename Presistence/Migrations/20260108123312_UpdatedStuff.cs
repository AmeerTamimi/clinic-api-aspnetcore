using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicAPI.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedStuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Patients",
                type: "TEXT",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Patients",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Permissions",
                table: "Patients",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Patients",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "Patients",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Doctors",
                type: "TEXT",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Doctors",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Permissions",
                table: "Doctors",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "Doctors",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 1,
                columns: new[] { "Age", "Email", "PasswordHash", "Permissions", "Roles", "YearOfExperience" },
                values: new object[] { 40, "ahmad.khalil@clinic.test", "HASH", "[\"patient:read\",\"patient:create\",\"patient:update\",\"patient:delete\",\"patient:read-appointments\",\"patient:manage-doctor\",\"patient:manage-appointments\",\"doctor:read\",\"doctor:create\",\"doctor:update\",\"doctor:delete\",\"doctor:read-patients\",\"doctor:read-appointments\",\"doctor:delete-patient\",\"doctor:add-patient\",\"doctor:update-specialty\",\"appointment:read\",\"appointment:create\",\"appointment:update\",\"appointment:delete\",\"appointment:update-status\",\"appointment:manage-patient\",\"appointment:manage-doctor\",\"appointment:update-date\"]", "[\"Doctor\"]", 15 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 2,
                columns: new[] { "Age", "Email", "PasswordHash", "Permissions", "Roles", "YearOfExperience" },
                values: new object[] { 34, "rana.haddad@clinic.test", "HASH", "[]", "[\"Doctor\"]", 10 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 3,
                columns: new[] { "Age", "Email", "PasswordHash", "Permissions", "Roles", "YearOfExperience" },
                values: new object[] { 38, "omar.safi@clinic.test", "HASH", "[]", "[\"Doctor\"]", 12 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 4,
                columns: new[] { "Age", "Email", "PasswordHash", "Permissions", "Roles", "YearOfExperience" },
                values: new object[] { 33, "lina.barghouti@clinic.test", "HASH", "[]", "[\"Doctor\"]", 8 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 5,
                columns: new[] { "Age", "Email", "PasswordHash", "Permissions", "Roles", "YearOfExperience" },
                values: new object[] { 45, "yousef.nassar@clinic.test", "HASH", "[]", "[\"Doctor\"]", 20 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 6,
                columns: new[] { "Age", "Email", "PasswordHash", "Permissions", "Roles", "YearOfExperience" },
                values: new object[] { 41, "hala.masri@clinic.test", "HASH", "[]", "[\"Doctor\"]", 16 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 7,
                columns: new[] { "Age", "Email", "PasswordHash", "Permissions", "Roles", "YearOfExperience" },
                values: new object[] { 36, "tariq.qasem@clinic.test", "HASH", "[]", "[\"Doctor\"]", 11 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 8,
                columns: new[] { "Age", "Email", "PasswordHash", "Permissions", "Roles", "YearOfExperience" },
                values: new object[] { 29, "maya.ayyad@clinic.test", "HASH", "[]", "[\"Doctor\"]", 5 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 9,
                columns: new[] { "Age", "Email", "PasswordHash", "Permissions", "Roles", "YearOfExperience" },
                values: new object[] { 39, "nabil.salameh@clinic.test", "HASH", "[]", "[\"Doctor\"]", 13 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 10,
                columns: new[] { "Age", "Email", "PasswordHash", "Permissions", "Roles", "YearOfExperience" },
                values: new object[] { 32, "sara.zaid@clinic.test", "HASH", "[]", "[\"Doctor\"]", 7 });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 1,
                columns: new[] { "Email", "PasswordHash", "Permissions", "Phone", "Roles" },
                values: new object[] { "ameer.tamimi@clinic.test", "HASH", "[\"patient:read\",\"patient:create\",\"patient:update\",\"patient:delete\",\"patient:read-appointments\",\"patient:manage-doctor\",\"patient:manage-appointments\",\"doctor:read\",\"doctor:create\",\"doctor:update\",\"doctor:delete\",\"doctor:read-patients\",\"doctor:read-appointments\",\"doctor:delete-patient\",\"doctor:add-patient\",\"doctor:update-specialty\",\"appointment:read\",\"appointment:create\",\"appointment:update\",\"appointment:delete\",\"appointment:update-status\",\"appointment:manage-patient\",\"appointment:manage-doctor\",\"appointment:update-date\"]", "+970599001001", "[\"Patient\"]" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 2,
                columns: new[] { "Email", "PasswordHash", "Permissions", "Phone", "Roles" },
                values: new object[] { "hareth.shoman@clinic.test", "HASH", "[]", "+970599001002", "[\"Patient\"]" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 3,
                columns: new[] { "Email", "PasswordHash", "Permissions", "Phone", "Roles" },
                values: new object[] { "elyas.najeh@clinic.test", "HASH", "[]", "+970599001003", "[\"Patient\"]" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 4,
                columns: new[] { "Email", "PasswordHash", "Permissions", "Phone", "Roles" },
                values: new object[] { "mariam.yasin@clinic.test", "HASH", "[]", "+970599001004", "[\"Patient\"]" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 5,
                columns: new[] { "Email", "PasswordHash", "Permissions", "Phone", "Roles" },
                values: new object[] { "kareem.abulail@clinic.test", "HASH", "[]", "+970599001005", "[\"Patient\"]" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 6,
                columns: new[] { "Email", "PasswordHash", "Permissions", "Phone", "Roles" },
                values: new object[] { "noor.said@clinic.test", "HASH", "[]", "+970599001006", "[\"Patient\"]" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 7,
                columns: new[] { "Email", "PasswordHash", "Permissions", "Phone", "Roles" },
                values: new object[] { "zaid.qamhieh@clinic.test", "HASH", "[]", "+970599001007", "[\"Patient\"]" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 8,
                columns: new[] { "Email", "PasswordHash", "Permissions", "Phone", "Roles" },
                values: new object[] { "habeeb.ahmad@clinic.test", "HASH", "[]", "+970599001008", "[\"Patient\"]" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 9,
                columns: new[] { "Email", "PasswordHash", "Permissions", "Phone", "Roles" },
                values: new object[] { "waleed.noubani@clinic.test", "HASH", "[]", "+970599001009", "[\"Patient\"]" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 10,
                columns: new[] { "Email", "PasswordHash", "Permissions", "Phone", "Roles" },
                values: new object[] { "ruba.katout@clinic.test", "HASH", "[]", "+970599001010", "[\"Patient\"]" });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Email",
                table: "Doctors",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Doctors_Email",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Doctors");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 1,
                columns: new[] { "Age", "YearOfExperience" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 2,
                columns: new[] { "Age", "YearOfExperience" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 3,
                columns: new[] { "Age", "YearOfExperience" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 4,
                columns: new[] { "Age", "YearOfExperience" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 5,
                columns: new[] { "Age", "YearOfExperience" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 6,
                columns: new[] { "Age", "YearOfExperience" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 7,
                columns: new[] { "Age", "YearOfExperience" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 8,
                columns: new[] { "Age", "YearOfExperience" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 9,
                columns: new[] { "Age", "YearOfExperience" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 10,
                columns: new[] { "Age", "YearOfExperience" },
                values: new object[] { 0, 0 });
        }
    }
}
