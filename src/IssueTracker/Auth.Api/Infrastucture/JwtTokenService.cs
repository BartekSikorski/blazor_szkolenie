using Auth.Api.Domain;
using Auth.Api.Domain.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Auth.Api.Infrastucture;


// PM> Install-Package System.IdentityModel.Tokens.Jwt
public class JwtTokenService : ITokenService
{
    public string Create(UserIdentity userIdentity)
    {
        List<Claim> claims = GetClaims(userIdentity);

        var secretKey = "your-256-bit-secret-your-256-bit-secret-your-256-bit-secret";
        var key = Encoding.ASCII.GetBytes(secretKey);

        var credentials = new SymmetricSecurityKey(key);
        var signingCredentials = new SigningCredentials(credentials, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: signingCredentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;



    }

    private static List<Claim> GetClaims(UserIdentity userIdentity)
    {
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, userIdentity.Username));
        claims.Add(new Claim(ClaimTypes.Email, userIdentity.Email));
        return claims;
    }
}
