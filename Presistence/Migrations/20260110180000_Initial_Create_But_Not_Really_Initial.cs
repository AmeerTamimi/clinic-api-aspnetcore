using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClinicAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Create_But_Not_Really_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Permissions = table.Column<string>(type: "TEXT", nullable: false),
                    Roles = table.Column<string>(type: "TEXT", nullable: false),
                    RefreshTokenHash = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Salary = table.Column<decimal>(type: "TEXT", nullable: false),
                    Specialty = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    YearOfExperience = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Doctors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    RefreshTokenHash = table.Column<string>(type: "TEXT", nullable: false),
                    RefreshTokenId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Expires = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.RefreshTokenHash);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RiskLevel = table.Column<int>(type: "INTEGER", nullable: true),
                    BloodType = table.Column<int>(type: "INTEGER", nullable: true),
                    Allergies = table.Column<string>(type: "TEXT", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    DoctorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Patients_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    DoctorUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDone = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Symptoms = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Medicine = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Diagnostic = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Date = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorUserId",
                        column: x => x.DoctorUserId,
                        principalTable: "Doctors",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientUserId",
                        column: x => x.PatientUserId,
                        principalTable: "Patients",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Age", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Permissions", "Phone", "RefreshTokenHash", "Roles", "Type" },
                values: new object[,]
                {
                    { 1, 40, new DateTimeOffset(new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ahmad.khalil@clinic.test", "Ahmad", "Khalil", "HASH", "[\"patient:read\",\"patient:create\",\"patient:update\",\"patient:delete\",\"patient:read-appointments\",\"patient:manage-doctor\",\"patient:manage-appointments\",\"doctor:read\",\"doctor:create\",\"doctor:update\",\"doctor:delete\",\"doctor:read-patients\",\"doctor:read-appointments\",\"doctor:delete-patient\",\"doctor:add-patient\",\"doctor:update-specialty\",\"appointment:read\",\"appointment:create\",\"appointment:update\",\"appointment:delete\",\"appointment:update-status\",\"appointment:manage-patient\",\"appointment:manage-doctor\",\"appointment:update-date\"]", "+970599000001", null, "[\"Doctor\"]", 2 },
                    { 2, 34, new DateTimeOffset(new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "rana.haddad@clinic.test", "Rana", "Haddad", "HASH", "[]", "+970599000002", null, "[\"Doctor\"]", 2 },
                    { 3, 38, new DateTimeOffset(new DateTime(2025, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "omar.safi@clinic.test", "Omar", "Safi", "HASH", "[]", "+970599000003", null, "[\"Doctor\"]", 2 },
                    { 4, 33, new DateTimeOffset(new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "lina.barghouti@clinic.test", "Lina", "Barghouti", "HASH", "[]", "+970599000004", null, "[\"Doctor\"]", 2 },
                    { 5, 45, new DateTimeOffset(new DateTime(2025, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "yousef.nassar@clinic.test", "Yousef", "Nassar", "HASH", "[]", "+970599000005", null, "[\"Doctor\"]", 2 },
                    { 6, 41, new DateTimeOffset(new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "hala.masri@clinic.test", "Hala", "Masri", "HASH", "[]", "+970599000006", null, "[\"Doctor\"]", 2 },
                    { 7, 36, new DateTimeOffset(new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "tariq.qasem@clinic.test", "Tariq", "Qasem", "HASH", "[]", "+970599000007", null, "[\"Doctor\"]", 2 },
                    { 8, 29, new DateTimeOffset(new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "maya.ayyad@clinic.test", "Maya", "Ayyad", "HASH", "[]", "+970599000008", null, "[\"Doctor\"]", 2 },
                    { 9, 39, new DateTimeOffset(new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "nabil.salameh@clinic.test", "Nabil", "Salameh", "HASH", "[]", "+970599000009", null, "[\"Doctor\"]", 2 },
                    { 10, 32, new DateTimeOffset(new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "sara.zaid@clinic.test", "Sara", "Zaid", "HASH", "[]", "+970599000010", null, "[\"Doctor\"]", 2 },
                    { 11, 20, new DateTimeOffset(new DateTime(2025, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ameer.tamimi@clinic.test", "Ameer", "Tamimi", "HASH", "[\"patient:read\",\"patient:create\",\"patient:update\",\"patient:delete\",\"patient:read-appointments\",\"patient:manage-doctor\",\"patient:manage-appointments\",\"doctor:read\",\"doctor:create\",\"doctor:update\",\"doctor:delete\",\"doctor:read-patients\",\"doctor:read-appointments\",\"doctor:delete-patient\",\"doctor:add-patient\",\"doctor:update-specialty\",\"appointment:read\",\"appointment:create\",\"appointment:update\",\"appointment:delete\",\"appointment:update-status\",\"appointment:manage-patient\",\"appointment:manage-doctor\",\"appointment:update-date\"]", "+970599001001", null, "[\"Patient\"]", 1 },
                    { 12, 21, new DateTimeOffset(new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "hareth.shoman@clinic.test", "Hareth", "Shoman", "HASH", "[]", "+970599001002", null, "[\"Patient\"]", 1 },
                    { 13, 22, new DateTimeOffset(new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "elyas.najeh@clinic.test", "Elyas", "Najeh", "HASH", "[]", "+970599001003", null, "[\"Patient\"]", 1 },
                    { 14, 30, new DateTimeOffset(new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "mariam.yasin@clinic.test", "Mariam", "Yasin", "HASH", "[]", "+970599001004", null, "[\"Patient\"]", 1 },
                    { 15, 28, new DateTimeOffset(new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "kareem.abulail@clinic.test", "Kareem", "AbuLail", "HASH", "[]", "+970599001005", null, "[\"Patient\"]", 1 },
                    { 16, 19, new DateTimeOffset(new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "noor.said@clinic.test", "Noor", "Said", "HASH", "[]", "+970599001006", null, "[\"Patient\"]", 1 },
                    { 17, 23, new DateTimeOffset(new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "zaid.qamhieh@clinic.test", "Zaid", "Qamhieh", "HASH", "[]", "+970599001007", null, "[\"Patient\"]", 1 },
                    { 18, 24, new DateTimeOffset(new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "habeeb.ahmad@clinic.test", "Habeeb", "Ahmad", "HASH", "[]", "+970599001008", null, "[\"Patient\"]", 1 },
                    { 19, 26, new DateTimeOffset(new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "waleed.noubani@clinic.test", "Waleed", "Noubani", "HASH", "[]", "+970599001009", null, "[\"Patient\"]", 1 },
                    { 20, 29, new DateTimeOffset(new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ruba.katout@clinic.test", "Ruba", "Katout", "HASH", "[]", "+970599001010", null, "[\"Patient\"]", 1 }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "UserId", "Salary", "Specialty", "YearOfExperience" },
                values: new object[,]
                {
                    { 1, 0m, "Cardiology", 15 },
                    { 2, 0m, "Dermatology", 10 },
                    { 3, 0m, "Orthopedics", 12 },
                    { 4, 0m, "Pediatrics", 8 },
                    { 5, 0m, "ENT", 20 },
                    { 6, 0m, "Neurology", 16 },
                    { 7, 0m, "Cardiology", 11 },
                    { 8, 0m, "GeneralPractice", 5 },
                    { 9, 0m, "Neurology", 13 },
                    { 10, 0m, "Orthopedics", 7 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "UserId", "Allergies", "BloodType", "DoctorId", "Note", "RiskLevel" },
                values: new object[,]
                {
                    { 11, null, null, 7, null, null },
                    { 12, null, null, 2, null, null },
                    { 13, null, null, 3, null, null },
                    { 14, null, null, 5, null, null },
                    { 15, null, null, 1, null, null },
                    { 16, null, null, 8, null, null },
                    { 17, null, null, 4, null, null },
                    { 18, null, null, 3, null, null },
                    { 19, null, null, 7, null, null },
                    { 20, null, null, 6, null, null }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "CreatedAt", "Date", "Diagnostic", "DoctorUserId", "Medicine", "PatientUserId", "Symptoms" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2025, 12, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 12, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), null, 7, null, 11, "Headache" });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "CreatedAt", "Date", "Diagnostic", "DoctorUserId", "IsDone", "Medicine", "PatientUserId", "Symptoms" },
                values: new object[] { 2, new DateTimeOffset(new DateTime(2025, 12, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 12, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "Dermatitis", 2, true, "Cream", 12, "Skin rash" });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "CreatedAt", "Date", "Diagnostic", "DoctorUserId", "Medicine", "PatientUserId", "Symptoms" },
                values: new object[,]
                {
                    { 3, new DateTimeOffset(new DateTime(2025, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 12, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), null, 3, null, 13, "Knee pain" },
                    { 4, new DateTimeOffset(new DateTime(2025, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 12, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), null, 5, null, 14, "Sore throat" },
                    { 5, new DateTimeOffset(new DateTime(2025, 12, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 12, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), null, 1, null, 15, "Chest pain" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "CreatedAt", "Date", "Diagnostic", "DoctorUserId", "IsDone", "Medicine", "PatientUserId", "Symptoms" },
                values: new object[] { 6, new DateTimeOffset(new DateTime(2025, 12, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 12, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "Dry eye", 8, true, "Eye drops", 16, "Blurred vision" });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "CreatedAt", "Date", "Diagnostic", "DoctorUserId", "Medicine", "PatientUserId", "Symptoms" },
                values: new object[,]
                {
                    { 7, new DateTimeOffset(new DateTime(2025, 12, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 12, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), null, 4, null, 17, "Fever" },
                    { 8, new DateTimeOffset(new DateTime(2025, 12, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 12, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), null, 3, null, 18, "Back pain" },
                    { 9, new DateTimeOffset(new DateTime(2025, 12, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 12, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), null, 7, null, 19, "Dizziness" },
                    { 10, new DateTimeOffset(new DateTime(2025, 12, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 12, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), null, 6, null, 20, "Migraine" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorUserId",
                table: "Appointments",
                column: "DoctorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientUserId",
                table: "Appointments",
                column: "PatientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorId",
                table: "Patients",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
