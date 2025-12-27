namespace ClinicAPI.CustomExceptions
{
    public class AuthorizationException : ApiException
    {
        public AuthorizationException(string message = "you have no access to this resource")
                : base(401, "Not Authorized", "https://httpstatuses.com/401", message)
        {
        }
    }
}
