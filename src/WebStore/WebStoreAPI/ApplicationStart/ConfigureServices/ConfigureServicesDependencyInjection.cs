using WebStore.Logic.Interfaces;
using WebStore.Logic.Services;

namespace WebStore.API.ApplicationStart.ConfigureServices;

public static class ConfigureServicesDependencyInjection
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IUsersProfileService, UsersProfileService>();
        services.AddScoped<IProductsService, ProductsService>();
        services.AddScoped<IOrdersService, OrdersService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPaymentsService, PaymentsService>();
        services.AddScoped<IPaymentsEventService, PaymentsEventService>();

        return services;
    }
}