using System.Security.Claims;

namespace WebStore.Logic.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
}