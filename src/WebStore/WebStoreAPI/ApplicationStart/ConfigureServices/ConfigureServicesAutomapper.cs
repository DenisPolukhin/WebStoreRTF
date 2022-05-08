using WebStore.Logic.Profiles;

namespace WebStore.API.ApplicationStart.ConfigureServices;

public static class ConfigureServicesAutoMapper
{
    public static IServiceCollection AddAutoMapperService(this IServiceCollection services)
    {
        services.AddAutoMapper(conf =>
        {
            conf.AddProfile<UserProfile>();
            conf.AddProfile<ProductProfile>();
            conf.AddProfile<CategoryProfile>();
        });

        return services;
    }
}