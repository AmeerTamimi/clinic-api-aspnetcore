namespace ClinicAPI.CustomExceptions
{
    public class ForbiddenException : ApiException
    {
        public ForbiddenException(string message = "you can't access the resource")
                : base(403, "Forbidden", "https://httpstatuses.com/403", message)
        {
        }
    }
}
