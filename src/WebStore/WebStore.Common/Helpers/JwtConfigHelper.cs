using Microsoft.Extensions.Configuration;
using WebStore.Common.Models;

namespace WebStore.Common.Helpers;

public static class JwtConfigHelper
{
    public static JwtConfig GetJwtConfig(this IConfiguration configuration)
    {
        return configuration.GetSection(nameof(JwtConfig)).Get<JwtConfig>();
    }
}