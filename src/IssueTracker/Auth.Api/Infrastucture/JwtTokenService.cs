using Auth.Api.Domain;
using Auth.Api.Domain.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Auth.Api.Infrastucture;


public class JwtTokenServiceOptions
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}

// PM> Install-Package System.IdentityModel.Tokens.Jwt
public class JwtTokenService : ITokenService
{
    private readonly JwtTokenServiceOptions options;

    public JwtTokenService(IOptions<JwtTokenServiceOptions> options)
    {
        this.options = options.Value;
    }

    public string Create(UserIdentity userIdentity)
    {
        List<Claim> claims = GetClaims(userIdentity);

        var secretKey = options.SecretKey;
        var key = Encoding.ASCII.GetBytes(secretKey);

        var credentials = new SymmetricSecurityKey(key);
        var signingCredentials = new SigningCredentials(credentials, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,
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
