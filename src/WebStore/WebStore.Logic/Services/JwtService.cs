using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebStore.Common.Helpers;
using WebStore.Logic.Interfaces;

namespace WebStore.Logic.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var jwtConfig = _configuration.GetJwtConfig();
        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(jwtConfig.LifeTime),
            signingCredentials: new SigningCredentials(jwtConfig.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}