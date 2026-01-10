# ClinicAPI

ASP.NET Core Web API for a simple clinic system: **Patients, Doctors, Appointments**.

Built while following Eng. Issam’s ASP.NET Core Web API course, then extended with my own structure (**Controllers / Services / Repos**, DTOs, pagination, FluentValidation, global exception handling, grouped DI registrations, Options pattern).

---

## What’s implemented

### Core API
- Controllers: **Patients**, **Doctors**, **Appointments**, **Tokens**
- Domain models: **User**, **Patient**, **Doctor**, **Appointment**, **RefreshTokenModel**
- Service layer (`I*Service` + `*Service`) for business flow + orchestration 
- Repository layer (`I*Repo` + `*Repo`) using **EF Core + DbContext** 

### Security (JWT + Refresh Tokens)
- **JWT Bearer authentication**
- **Access token** (short-lived) used to call secured endpoints
- **Refresh token flow** (server-side stored) 
  - Refresh token is generated as a **real random token**
  - Only the **hash** is stored in DB (raw token is never stored) 
  - Supports **1 refresh token per user** (enforced by unique `UserId`) 
  - Refresh token entity uses `RefreshTokenId` as the **primary key** (DB-friendly), and `RefreshTokenHash` as a **unique indexed** column ✅
- Policy-based authorization using permissions (`Permissions/Permission.cs`) + registrations


### Async + Cancellation
- All endpoints are **async**
- `CancellationToken` flows from controllers → services → repos → EF Core calls
- EF Core async operations use `...Async(ct)` (e.g. `ToListAsync(ct)`, `SaveChangesAsync(ct)`)

### Persistence (EF Core)
- `ClinicDbContext` under `Persistence/ClinicDbContext.cs`
- Migrations under `Persistence/Migrations/`
- Entity configs under `Persistence/DbConfigurations/` 

### DTOs
- `Requests/` for create/update + token requests
- `Query/` for query contracts (paging/filtering inputs)
- `Responses/` for output DTOs + paging wrapper (`PagedResult`)

### Validation + Error Handling
- Validation: **FluentValidation** (`Validators/`)
- Error handling: custom exceptions + global exception handler (`CustomExceptions/GlobalExceptionHandler.cs`) ✅
- Pagination support for list endpoints
- HTTP test file: `ClinicAPI.http`

---

## Project Structure

This is the current solution layout (same folder grouping used in the code):

### Folder purpose (quick map)
- `Configurations/` → Options pattern classes + setup (JWT, clinic settings)
- `Controllers/` → HTTP endpoints
- `CustomExceptions/` → clean error model + global handler
- `Enums/` → shared enums (UserType, RiskLevel, BloodType, etc.)
- `Models/` → domain entities (EF Core)
- `Permissions/` → permission constants used in policies
- `Persistence/`
  - `DbConfigurations/` → EF model configurations + seeding
  - `Migrations/` → EF migrations
  - `ClinicDbContext.cs` → DB context
- `Query/` → paging/filter inputs
- `Registrations/` → grouped DI registrations (auth, validators, infrastructure, business)
- `Repositories/` → data access layer
- `Requests/` + `Responses/` → DTOs
- `Service/` → business logic layer
- `Validators/` → FluentValidation validators

---

## Options Pattern
Configuration is strongly-typed and isolated in `Configurations/`:
- `ClinicSettings.cs`
- `JwtSettings.cs`
- `JwtBearerConfigurations.cs`

This keeps config clean, avoids magic strings, and makes Program.cs lighter

---

## Endpoints

### Patients
- `GET /patients?page=&pageSize=&includeAppointments=`
- `GET /patients/{patientId:int}`
- `GET /patients/{patientId:int}/appointments`
- `POST /patients`
- `PUT /patients/{patientId:int}`
- `DELETE /patients/{patientId:int}`

### Doctors
- `GET /doctors?page=&pageSize=&includePatients=&includeAppointments=`
- `GET /doctors/{doctorId:int}`
- `GET /doctors/{doctorId:int}/patients?includeAppointments=`
- `GET /doctors/{doctorId:int}/appointments`
- `POST /doctors`
- `PUT /doctors/{doctorId:int}`
- `DELETE /doctors/{doctorId:int}`

### Appointments
- `GET /appointments?page=&pageSize=`
- `GET /appointments/{appointmentId:int}`
- `POST /appointments`
- `PUT /appointments/{appointmentId:int}`
- `DELETE /appointments/{appointmentId:int}`

### Tokens
- Token endpoints exist under `TokensController` (see `Controllers/TokensController.cs` / `ClinicAPI.http`)
- Login issues **access + refresh**
- Refresh issues **new access + refresh**

---

## Run

```bash
dotnet restore
dotnet run
