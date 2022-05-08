using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using WebStore.Database.Models.Enums;

namespace WebStore.API.ApplicationStart.ConfigureServices;

public static class ConfigureServicesAuthorization
{
    public static IServiceCollection AddAuthorizationService(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

            options.AddPolicy("Administrator", policy => policy
                .RequireRole(nameof(Role.Administrator))
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser());
        });

        return services;
    }
}