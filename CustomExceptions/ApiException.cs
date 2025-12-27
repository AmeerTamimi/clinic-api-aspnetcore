namespace ClinicAPI.CustomExceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }

        protected ApiException(int statusCode, string title, string type, string message) : base(message)
        {
            StatusCode = statusCode;
            Title = title;
            Type = type;
        }
    }
}
