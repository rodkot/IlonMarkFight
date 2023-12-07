using System.Text.Json;
using OpponentsWebApp.Dto;
using OpponentsWebApp.Exceptions;

namespace OpponentsWebApp;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
        catch (BadDeckLength e)
        {
            context.Response.StatusCode = 400;
            var err = new ErrorDto { Message = "bad-deck-length"};
            //var errResp = new ErrorResponse { Errors = new List<ErrorDto> { err } };
            await JsonSerializer.SerializeAsync(context.Response.Body, err);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = 500;
            var err = new ErrorDto { Message = "internal-error"};
           // var errResp = new ErrorResponse { Errors = new List<ErrorDto> { err } };
            await JsonSerializer.SerializeAsync(context.Response.Body, err);
        } 
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(
        this IApplicationBuilder builder)
    {
       // Console.WriteLine("Use custom middleware");
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}
