# ClinicAPI

ASP.NET Core Web API for a simple clinic system: **Patients, Doctors, Appointments**.

Built while following Eng. Issam’s ASP.NET Core Web API course, then extended with my own structure (**Controllers / Services / Repos**, DTOs, pagination, FluentValidation, global exception handling, grouped DI registrations).

---

## What’s implemented 

### Core API
- Controllers: **Patients**, **Doctors**, **Appointments**, **Tokens**
- Domain models: **Patient**, **Doctor**, **Appointment**, **User**, **RefreshTokenModel**
- Service layer (`I*Service` + `*Service`) for business flow + orchestration
- Repository layer (`I*Repo` + `*Repo`) using **EF Core + DbContext**

### Security (JWT) 
- **JWT Bearer authentication**
- **Access token** support (used to call secured endpoints)
- **Refresh token model** exists (`Models/RefreshTokenModel.cs`)
- Policy-based authorization using permissions (`Permissions/Permission.cs`) + registrations

> If you call a secured endpoint without a valid access token → **401 Unauthorized** ❌

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
- Error handling: custom exceptions + global exception handler (`CustomExceptions/GlobalExceptionHandler.cs`)
- Pagination support for list endpoints
- HTTP test file: `ClinicAPI.http`

---

## Options Pattern
Configuration is strongly-typed and isolated in `Configurations/`:
- `ClinicSettings.cs`
- `JwtSettings.cs`

This keeps config clean, avoids magic strings, and makes Program.cs lighter.

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

---

## Run

```bash
dotnet restore
dotnet run
