using Microsoft.AspNetCore.Diagnostics;

namespace EmployeeHubAPI.Exceptions
{
    public class ExceptionHandler : IExceptionHandler
    {
        public ExceptionHandler()
        {
                
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            (int statusCode, string errorMessage) = exception switch
            {
                BadRequestException badRequestException => (403, badRequestException.Message),
                NotFoundException notFoundException => (404, notFoundException.Message),
                _ => (500, "Ooops, we encountered problem. Come back later.")
            };

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(errorMessage);

            return true;
        }
    }
}
