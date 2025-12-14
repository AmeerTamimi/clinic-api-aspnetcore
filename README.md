# clinic-api-aspnetcore

A small ASP.NET Core Web API for a clinic system:
Patients, Doctors, Appointments.

Built while following **Eng. Issam's ASP.NET Core Web API course**, and extended with my own structure and features
(DI/IoC, repositories, services, middleware, routing, etc.).

## Current Status

- Models for Patient, Doctor, Appointment
- DbContext (`ClinicDbContext`)
- Repository layer (`I*Repo` + `*Repo`)
- Service layer (`I*Service` + `*Service`)
- Grouped DI registrations (Business + Infrastructure)
- Configuration bound with Options pattern
- Controller routing + route parameters and constraints
- Custom middlewares (logging/timing, response header, blocking)

## Middlewares

This project includes a few simple custom middlewares:

- **Logging + timing**
  - Logs `Method`, `Path`, `StatusCode`, and request time (ms) to the console for every request.
- **Response header**
  - Adds `X-Name: Ameer` to every response header.
  - Stores `"Name"` in `HttpContext.Items` so other middleware/endpoints can read it.
- **Early block**
  - Blocks access to `/admin/*` routes (returns `403 Forbidden`).

## Routing

Routing is done using **Controllers** + attribute routing.

- Uses **route parameters** like `{id}`
- Uses **route constraints** like `{id:int}` to ensure IDs are valid integers
- Includes **nested routes** under doctors (patients/appointments for a doctor)

### Endpoints

**Patients**
- `GET /Patients`
- `GET /Patients/{id:int}`

**Doctors**
- `GET /Doctors`
- `GET /Doctors/{id:int}`
- `GET /Doctors/{id:int}/patients`
- `GET /Doctors/{id:int}/appointments`

**Appointments**
- `GET /Appointments`
- `GET /Appointments/{id:int}`

## Project Structure

- `Models/` — domain models (Patient, Doctor, Appointment)
- `DB/` — `ClinicDbContext`
- `Repository/` — repository interfaces and implementations
- `Service/` — business logic services
- `Controllers/` — API endpoints (routing + actions)
- `GroupedRegistrations/` — extension methods for DI setup
- `Configuration/` — options pattern settings
- `Program.cs` — composition root (DI + middleware + routing)

## Run

```bash
dotnet restore
dotnet run
