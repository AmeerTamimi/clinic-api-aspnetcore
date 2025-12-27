namespace ClinicAPI.CustomExceptions
{
    public class ServerException : ApiException
    {
        public ServerException(string message = "Server Error") : 
            base(500 , "Server Error" , "https://httpstatuses.com/500" , message)
        {

        }
    }
}
