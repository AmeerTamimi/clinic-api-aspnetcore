# clinic-api-aspnetcore

ASP.NET Core Web API for a simple clinic system:
**Patients**, **Doctors**, **Appointments**.

Built while following **Eng. Issam's ASP.NET Core Web API course**, then extended with my own structure
(controllers/services/repos, model binding, DTOs, pagination, DI grouping).

## What’s implemented

- Controllers: `Patients`, `Doctors`, `Appointments`
- Domain models: `Patient`, `Doctor`, `Appointment`
- Repository layer (`I*Repo` + `*Repo`) using **in-memory seeded data** (no real DB yet)
- Service layer (`I*Service` + `*Service`) for business flow + validation
- DTOs:
  - `Requests/` for create/update contracts
  - `Query/` for query contracts
  - `Responses/` for output DTOs + mapping methods
- Pagination helper: `PagedResult<T>`
- Helper endpoint: `GET /route-table`
- HTTP test file: `ClinicAPI.http`

> Note: `ClinicDbContext` exists under `Persistence/`, but current repos use in-memory data for now.

## Model Binding

- **Route params**: `/{id:int}`
- **Query string**: `?page=&pageSize=&includeAppointments=`
- **Body**: JSON `application/json`

## Pagination

List endpoints support:
- `page` (default 1)
- `pageSize` (clamped)
- `includeAppointments` (patients + doctor patients)

Example:
- `GET /patients?page=1&pageSize=10&includeAppointments=false`

## Endpoints

### Patients
- `GET /patients?page=&pageSize=&includeAppointments=`
- `GET /patients/{patientId:int}`
- `GET /patients/{patientId:int}/appointments`
- `POST /patients`
- `PUT /patients/{patientId:int}`
- `DELETE /patients/{patientId:int}`

### Doctors
- `GET /doctors?page=&pageSize=`
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

## Project Structure

- `Controllers/` — endpoints
- `Models/` — domain models
- `Persistence/` — `ClinicDbContext` (placeholder for later DB)
- `Repositories/` — repo interfaces + in-memory implementations
- `Service/` — business logic + orchestration
- `Requests/` — create/update DTOs
- `Query/` — query DTOs
- `Responses/` — response DTOs + `PagedResult<T>`
- `GroupedRegistrations/` — grouped DI registrations
- `Program.cs` — composition root (DI + middleware + routing)

## Run

```bash
dotnet restore
dotnet run
