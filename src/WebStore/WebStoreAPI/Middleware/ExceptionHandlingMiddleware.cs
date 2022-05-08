using System.Net;
using WebStore.Common.Exceptions;

namespace WebStore.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await ExceptionHandler(context, ex);
        }
    }

    public async Task ExceptionHandler(HttpContext context, Exception ex)
    {
        _logger.LogError(ex.Message);
        var errors = new List<string> {ex.Message};
        var code = ex switch
        {
            EntityFindException _ => HttpStatusCode.NotFound,
            _ => HttpStatusCode.InternalServerError
        };

        await context.Response.WriteAsJsonAsync(new {Code = code, Errors = errors});
    }
}