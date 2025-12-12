# clinic-api-aspnetcore

A small ASP.NET Core Web API for a clinic system:
Patients, Doctors, Appointments.

Built while following **Eng. Issam's ASP.NET Core Web API course**, and extended with my own structure and features
(DI/IoC, repositories, services, middleware, etc.).

## Current Status

- Models for Patient, Doctor, Appointment
- DbContext (`ClinicDbContext`)
- Repository layer (`I*Repo` + `*Repo`)
- Service layer (`I*Service` + `*Service`)
- Grouped DI registrations (Business + Infrastructure)
- Configuration bound with Options pattern

## Project Structure

- `Models/` — domain models (Patient, Doctor, Appointment)
- `DB/` — `ClinicDbContext`
- `Repository/` — repository interfaces and implementations
- `Service/` — business logic services
- `GroupedRegistrations/` — extension methods for DI setup
- `Program.cs` — composition root (DI + middleware + endpoints)

## Run

```bash
dotnet restore
dotnet run
