
using System.Net;
using TaggTimeline.Service.Exceptions;

namespace TaggTimeline.WebApi;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception e)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch(e)
            {
                case EntityNotFoundException notFound:
                    response.StatusCode = (int) HttpStatusCode.NotFound;
                    break;

                default:
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
            }

            var result = e.Message;
            await response.WriteAsJsonAsync(result);
        }
    
    }

}
