# clinic-api-aspnetcore

A small ASP.NET Core Web API for a clinic system:
Patients, Doctors, Appointments.

Built while following **Eng. Issam's ASP.NET Core Web API course**, and extended with my own structure
(DI/IoC, repositories, services, controllers, model binding, DTOs, etc.).

## What’s implemented

- Controllers for **Patients**, **Doctors**, **Appointments**
- Domain models: Patient, Doctor, Appointment
- Repository layer (`I*Repo` + `*Repo`) using **in-memory seeded fake data** (for learning / no real DB yet)
- Service layer (`I*Service` + `*Service`) for orchestration + business flow
- Request/Response contracts:
  - `Requests/` for input DTOs (create/update)
  - `Query/` for search/query DTOs
  - `Responses/` for output DTOs
- Response DTO mapping via `FromModel(...)` methods
- Pagination helper: `PagedResult<T>`
- Helper endpoint: `GET /route-table`

> Note: `ClinicDbContext` exists under `Persistence/`, but current repos use in-memory data for now.

## Model Binding Used

- **Route params** (identity): `/patients/{id:int}`, `/doctors/{id:int}`, `/appointments/{id:int}`
- **Query string** (search/list): `/patients/search`, `/doctors/search`, `/appointments/search`
- **Body binding** (create/update): JSON `application/json`

## Pagination

Paging is supported via `PagedResult<T>` and list/search endpoints can use query params like:
- `page` (default 1)
- `pageSize` (clamped to a reasonable max)

Example:
- `GET /patients?page=1&pageSize=10`

## Endpoints

### Patients
- `GET /patients`
- `GET /patients/{id:int}`
- `GET /patients/search`
- `POST /patients`
- `PUT /patients/{id:int}`

### Doctors
- `GET /doctors`
- `GET /doctors/{id:int}`
- `GET /doctors/search`
- `POST /doctors`
- `PUT /doctors/{id:int}`

### Appointments
- `GET /appointments`
- `GET /appointments/{id:int}`
- `GET /appointments/search`
- `POST /appointments`
- `PUT /appointments/{id:int}`

## Project Structure

- `Controllers/` — API endpoints (routing + actions)
- `Models/` — domain models (Patient, Doctor, Appointment)
- `Persistence/` — `ClinicDbContext` (placeholder for later real DB work)
- `Repositories/` — repository interfaces and implementations (currently in-memory)
- `Service/` — business flow + orchestration
- `Requests/` — create/update request DTOs (base request included)
- `Query/` — search/query DTOs
- `Responses/` — response DTOs + `PagedResult<T>`
- `GroupedRegistrations/` — grouped DI registrations (Business/Infrastructure)
- `Program.cs` — composition root (DI + middleware + routing)

## Run

```bash
dotnet restore
dotnet run
