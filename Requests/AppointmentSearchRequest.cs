namespace ClinicAPI.Requests

{
    public class AppointmentSearchRequest
    {
        // For Search in General
        public string? Query { get; set; }

        // Pagination
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;

        // Sorting
        public List<string> SortBy { get; set; } = new List<string>() { "Date" };
        public bool SortDesc { get; set; } = false;

        // Filtering
        public DateOnly? From { get; set; }
        public DateOnly? To { get; set; }
    }
}
