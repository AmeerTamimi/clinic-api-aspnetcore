using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.CustomExceptions
{
    public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService , IHostEnvironment hostEnvironemnt) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var status = StatusCodes.Status500InternalServerError;
            if(exception  is ApiException apiException)
            {
                status = apiException.StatusCode;
            }

            httpContext.Response.StatusCode = status;

            var problemDetailsContext = new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Type = "",
                    Title = "Error Occured",
                    Detail = exception.Message,
                    Status = status
                }
            };

            if (hostEnvironemnt.IsDevelopment())
            {
                problemDetailsContext.ProblemDetails.Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}";
                problemDetailsContext.ProblemDetails.Type = exception.GetType().Name;

            }

            return await problemDetailsService.TryWriteAsync(problemDetailsContext);
        }
    }
}
