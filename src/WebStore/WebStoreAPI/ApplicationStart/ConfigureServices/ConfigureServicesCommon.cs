using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.Database.Models;
using WebStore.Database.Models.Entities;
using WebStore.Logic.Validators.User;

namespace WebStore.API.ApplicationStart.ConfigureServices;

public static class ConfigureServicesCommon
{
    public static IServiceCollection AddBaseService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
            .AddFluentValidation(conf => conf.RegisterValidatorsFromAssemblyContaining(typeof(LogInValidator)));
        services.AddEndpointsApiExplorer();
        services.AddDbContextFactory<DatabaseContext>(options => options.UseNpgsql(configuration
            .GetConnectionString("DefaultConnection"), op => op.UseNodaTime()));
        services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.User.AllowedUserNameCharacters = null;
            })
            .AddEntityFrameworkStores<DatabaseContext>();

        return services;
    }
}