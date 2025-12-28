# ClinicAPI

ASP.NET Core Web API for a simple clinic system: **Patients, Doctors, Appointments**.

Built while following Eng. Issam’s ASP.NET Core Web API course, then extended with my own structure (**Controllers / Services / Repos**, DTOs, pagination, FluentValidation, global exception handling, grouped DI registrations).

---

## What’s implemented

### Core API
- Controllers: **Patients**, **Doctors**, **Appointments**
- Domain models: **Patient**, **Doctor**, **Appointment**
- Service layer (`I*Service` + `*Service`) for business flow + orchestration
- Repository layer (`I*Repo` + `*Repo`) using **EF Core + DbContext** (real database)

### Async + Cancellation
- All endpoints are **async**
- `CancellationToken` is accepted in controllers and **propagated** through services → repos → EF Core calls
- EF Core async operations use `...Async(ct)` (e.g. `ToListAsync(ct)`, `SingleOrDefaultAsync(..., ct)`, `SaveChangesAsync(ct)`)

### Persistence (EF Core)
- `ClinicDbContext` under `Persistence/`
- Entity configurations per model using `IEntityTypeConfiguration<T>`
- Seed data via `HasData(...)` in configurations
- Relationships configured:
  - Patient ↔ Appointments
  - Doctor ↔ Appointments
  - Patient ↔ Doctor
- Soft delete pattern via `IsDeleted`

### DTOs
- `Requests/` for create/update contracts  
- `Query/` for query contracts  
- `Responses/` for output DTOs + mapping (`FromModel`) + paging results

### Validation + Error Handling
- Validation: **FluentValidation** (`Validators/`)
- Error handling: custom exceptions + global exception handler (`CustomExceptions/`)
- Pagination support for list endpoints
- Helper endpoint: `GET /route-table`
- HTTP test file: `ClinicAPI.http`

---

## Model binding quick notes
- Route params: `/{id:int}`
- Query string: `?page=&pageSize=&includeAppointments=`
- Body: JSON `application/json`

> If `includeAppointments=true`, related appointments are loaded using EF Core `Include(...)` (otherwise nav props stay empty).

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

---

## Run

```bash
dotnet restore
dotnet run
