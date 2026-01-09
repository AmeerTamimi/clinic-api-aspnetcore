namespace ClinicAPI.Permissions
{
    public class Permission
    {
        public const string ClaimType = "permission";

        public static class Patient
        {
            public const string Read = "patient:read";
            public const string Create = "patient:create";
            public const string Update = "patient:update";
            public const string Delete = "patient:delete";
            public const string ReadAppointments = "patient:read-appointments";
            public const string ManageDoctor = "patient:manage-doctor";
            public const string ManageAppointments = "patient:manage-appointments";
        }

        public static class Doctor
        {
            public const string Read = "doctor:read";
            public const string Create = "doctor:create";
            public const string Update = "doctor:update";
            public const string Delete = "doctor:delete";
            public const string ReadPatients = "doctor:read-patients";
            public const string ReadAppointments = "doctor:read-appointments";
            public const string DeletePatient = "doctor:delete-patient";
            public const string AddPatient = "doctor:add-patient";
            public const string UpdateSpecialty = "doctor:update-specialty";
        }

        public static class Appointment
        {
            public const string Read = "appointment:read";
            public const string Create = "appointment:create";
            public const string Update = "appointment:update";
            public const string Delete = "appointment:delete";
            public const string UpdateStatus = "appointment:update-status";
            public const string ManagePatient = "appointment:manage-patient";
            public const string ManageDoctor = "appointment:manage-doctor";
            public const string UpdateDate = "appointment:update-date";
        }
    }
}
