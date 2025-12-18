using ClinicAPI.Models;

namespace ClinicAPI.Repositories
{
    public class DoctorRepo : IDoctorRepo
    {
        private static DateTimeOffset now = DateTimeOffset.UtcNow;

        private static List<Doctor> _doctors = new List<Doctor>
        {
            new() { DoctorId = 1,  FirstName = "Ahmad",  LastName = "Khalil",    Specialist = "Cardiology",    Phone = "+970599000001", CreatedAt = now.AddDays(-60), IsDeleted = false },
            new() { DoctorId = 2,  FirstName = "Rana",   LastName = "Haddad",    Specialist = "Dermatology",   Phone = "+970599000002", CreatedAt = now.AddDays(-55), IsDeleted = false },
            new() { DoctorId = 3,  FirstName = "Omar",   LastName = "Safi",      Specialist = "Orthopedics",   Phone = "+970599000003", CreatedAt = now.AddDays(-50), IsDeleted = false },
            new() { DoctorId = 4,  FirstName = "Lina",   LastName = "Barghouti", Specialist = "Pediatrics",    Phone = "+970599000004", CreatedAt = now.AddDays(-45), IsDeleted = false },
            new() { DoctorId = 5,  FirstName = "Yousef", LastName = "Nassar",    Specialist = "ENT",           Phone = "+970599000005", CreatedAt = now.AddDays(-40), IsDeleted = false },
            new() { DoctorId = 6,  FirstName = "Hala",   LastName = "Masri",     Specialist = "Neurology",     Phone = "+970599000006", CreatedAt = now.AddDays(-35), IsDeleted = false },
            new() { DoctorId = 7,  FirstName = "Tariq",  LastName = "Qasem",     Specialist = "General",       Phone = "+970599000007", CreatedAt = now.AddDays(-30), IsDeleted = false },
            new() { DoctorId = 8,  FirstName = "Maya",   LastName = "Ayyad",     Specialist = "Ophthalmology", Phone = "+970599000008", CreatedAt = now.AddDays(-25), IsDeleted = false },
            new() { DoctorId = 9,  FirstName = "Nabil",  LastName = "Salameh",   Specialist = "Urology",       Phone = "+970599000009", CreatedAt = now.AddDays(-20), IsDeleted = false },
            new() { DoctorId = 10, FirstName = "Sara",   LastName = "Zaid",      Specialist = "Gynecology",    Phone = "+970599000010", CreatedAt = now.AddDays(-15), IsDeleted = false },
        };
        public Doctor GetDoctorById(int doctorId)
        {
            return _doctors.FirstOrDefault(d => d.DoctorId == doctorId && !d.IsDeleted)!;
        }

        public Doctor AddNewDoctor(Doctor newDoctor)
        {
            int newId = _doctors.Count == 0 ? 1 : _doctors.Max(d => d.DoctorId) + 1;

            newDoctor.DoctorId = newId;
            newDoctor.CreatedAt = DateTimeOffset.UtcNow;
            newDoctor.IsDeleted = false;

            _doctors.Add(newDoctor);
            return newDoctor;
        }
        public Doctor UpdateDoctor(Doctor doctor, int doctorId)
        {
            var toUpdate = _doctors.FirstOrDefault(d => d.DoctorId == doctorId && !d.IsDeleted)!;

            toUpdate.FirstName = doctor.FirstName;
            toUpdate.LastName = doctor.LastName;
            toUpdate.Specialist = doctor.Specialist;
            toUpdate.Phone = doctor.Phone;

            return toUpdate;
        }

        public bool DeleteDoctorById(int doctorId)
        {
            var toDelete = _doctors.FirstOrDefault(d => d.DoctorId == doctorId && !d.IsDeleted)!;
            toDelete.IsDeleted = true;
            return true;
        } 

        public int GetDoctorCount()
        {
            return _doctors.Count(d => !d.IsDeleted);
        }

        public List<Doctor> GetDoctorPage(int page, int pageSize)
        {
            return _doctors
                .Where(d => !d.IsDeleted)
                .OrderBy(d => d.FirstName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
