ClinicAPI

ASP.NET Core Web API for a simple clinic system: Patients, Doctors, Appointments.

Built while following Eng. Issam’s ASP.NET Core Web API course, then extended with my own structure (controllers/services/repos, DTOs, pagination, FluentValidation, global exception handling, grouped DI registrations).

What’s implemented

Controllers: Patients, Doctors, Appointments

Domain models: Patient, Doctor, Appointment

Repository layer (I*Repo + *Repo) using in-memory seeded data (no database yet)

Service layer (I*Service + *Service) for business flow + orchestration

DTOs:

Requests/ for create/update contracts

Query/ for query contracts

Responses/ for output DTOs + mapping

Validation: FluentValidation (Validators/)

Error handling: custom exceptions + global exception handling (CustomExceptions/)

Pagination support for list endpoints

Helper endpoint: GET /route-table

HTTP test file: ClinicAPI.http

Note: ClinicDbContext exists under Persistence/, but current repos use in-memory data.

Model Binding

Route params: /{id:int}

Query string: ?page=&pageSize=&includeAppointments=

Body: JSON application/json

Endpoints
Patients

GET /patients?page=&pageSize=&includeAppointments=

GET /patients/{patientId:int}

GET /patients/{patientId:int}/appointments

POST /patients

PUT /patients/{id:int}

DELETE /patients/{patientId:int}

Doctors

GET /doctors (via DoctorSearchRequest)

GET /doctors/{doctorId:int}

GET /doctors/{doctorId:int}/patients?includeAppointments=

GET /doctors/{doctorId:int}/appointments

POST /doctors

PUT /doctors/{id:int}

DELETE /doctors/{doctorId:int}

Appointments

GET /appointments?page=&pageSize=

GET /appointments/{appointmentId:int}

POST /appointments

PUT /appointments/{id:int}

DELETE /appointments/{appointmentId:int}

Project Structure
ClinicAPI/
│
├─ Configuration/          # config helpers / options (if used)
├─ Controllers/            # API endpoints
├─ CustomExceptions/       # custom exception types
├─ Enums/                  # enums used across the project
├─ GroupedRegistrations/   # grouped DI registrations (clean Program.cs)
├─ Models/                 # domain models (Patient, Doctor, Appointment)
├─ Persistence/            # DbContext placeholder / persistence layer (future DB)
├─ Query/                  # query DTOs (pagination, include flags…)
├─ Repositories/           # I*Repo + *Repo (in-memory impl currently)
├─ Requests/               # request DTOs (Create/Update)
├─ Responses/              # response DTOs + mapping + PagedResult
├─ Service/                # business logic (validation + orchestration)
├─ Validators/             # FluentValidation validators
│
├─ Program.cs
├─ appsettings.json
├─ ClinicAPI.http          # ready-to-run HTTP requests
└─ README.md

Run
dotnet restore
dotnet run