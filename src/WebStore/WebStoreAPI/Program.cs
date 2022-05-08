using Serilog;
using WebStore.API.ApplicationStart.ConfigureServices;
using WebStore.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddSwaggerService();
builder.Services.AddBaseService(builder.Configuration);
builder.Services.AddAuthenticationService(builder.Configuration);
builder.Services.AddAuthorizationService();
builder.Services.AddCustomServices();
builder.Services.AddAutoMapperService();

var app = builder.Build();

try
{
    Log.Information("Web host start");
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}