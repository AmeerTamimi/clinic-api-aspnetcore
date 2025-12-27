namespace ClinicAPI.CustomExceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message = "resource not found")
                : base(404, "Not Found", "https://httpstatuses.com/404", message)
        {
        }
    }
}
