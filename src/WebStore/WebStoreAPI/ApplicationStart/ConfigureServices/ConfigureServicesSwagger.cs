using Microsoft.OpenApi.Models;

namespace WebStore.API.ApplicationStart.ConfigureServices;

public static class ConfigureServicesSwagger
{
    public static IServiceCollection AddSwaggerService(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                        {Reference = new OpenApiReference {Id = "Bearer", Type = ReferenceType.SecurityScheme}},
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
}