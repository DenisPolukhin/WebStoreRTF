using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebStore.Common.Models;

public class JwtConfig
{
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public int LifeTime { get; set; }
    public string SecretKey { get; set; } = default!;

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
    }
}