namespace ClinicAPI.CustomExceptions
{
    public class ConflictException : ApiException
    {
        public ConflictException(string message = "Conflicted Data")
                : base(409, "Conflict", "https://httpstatuses.com/409", message)
        {
        }
    }
}
