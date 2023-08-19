using System.Net;

namespace TaskManagementSystemProject
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception excep)
            {
                // Handle the exception and return a generic error response
                await HandleExceptionAsync(context, excep);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string errorMessage = "An error generate.";

            return context.Response.WriteAsync(errorMessage);
        }
    }
}
