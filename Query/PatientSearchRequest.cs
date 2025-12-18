namespace ClinicAPI.Query
{
    public class PatientSearchRequest
    {
        // For Search in General
        public string? Query { get; set; } 

        // Pagination
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;

        // Sorting
        public List<string> SortBy { get; set; } = new List<string>() { "Name" };
        public bool SortDesc { get; set; } = false;

        // Filtering
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
    }
}
