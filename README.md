# clinic-api-aspnetcore

A small ASP.NET Core Web API for a clinic system:
Patients, Doctors, Appointments.

Built while following **Eng. Issam's ASP.NET Core Web API course**, and extended with my own structure
(DI/IoC, repositories, services, controllers, model binding, etc.).

## What’s implemented

- Controllers for **Patients**, **Doctors**, **Appointments**
- Domain models filled (entities)
- Repository layer (`I*Repo` + `*Repo`) filled (currently dummy/incomplete persistence logic)
- Service layer (`I*Service` + `*Service`) filled (DTO mapping + orchestration)
- Request/Response contracts:
  - `Requests/` for input DTOs (create/update/search)
  - `Responses/` for output DTOs
- Tested endpoints using `.http` requests (POST/PUT for all 3 entities)
- Helper endpoint: `GET /route-table`

## Model Binding Used

- **Route params** (identity): `/patients/{id:int}`, `/doctors/{id:int}`, `/appointments/{id:int}`
- **Query string** (search/list): `/patients/search`, `/doctors/search`, `/appointments/search`
- **Body binding** (create/update): JSON `application/json`

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
- `Persistence/` — `ClinicDbContext`
- `Repositories/` — repository interfaces and implementations
- `Service/` — business logic + DTO mapping
- `Requests/` — input DTOs (Create/Update/Search)
- `Responses/` — output DTOs
- `GroupedRegistrations/` — grouped DI registrations
- `Program.cs` — composition root (DI + middleware + routing)

## Run

```bash
dotnet restore
dotnet run
