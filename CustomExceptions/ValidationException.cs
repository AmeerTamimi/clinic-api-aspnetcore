namespace ClinicAPI.CustomExceptions
{
    public class ValidationException : ApiException
    {
        public ValidationException(string message = "invalid values")
                : base(400, "Bad request", "https://httpstatuses.com/400", message)
        {
        }
    }
}
