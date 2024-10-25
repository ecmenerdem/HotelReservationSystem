using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotelReservation.Application.Contracts.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace HotelReservation.Infrastructure.Security;

public class TokenService:ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateLoginToken(Guid userGUID, string kullaniciAdi,IEnumerable<Claim> additionalClaims=null)
    {
        var claims = new List<Claim>
        {
            new Claim("KullaniciGUID", userGUID.ToString()),
            new Claim("KullaniciAdi", kullaniciAdi)
        };
        if (additionalClaims != null)
        {
            claims.AddRange(additionalClaims);
        }
        var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:JWTKey") ?? throw new ArgumentNullException("Key Bilgisi Boş Geldi"));
        var tokenDescriptor = new JwtSecurityToken(
            expires: DateTime.Now.AddDays(30),
            claims: claims,
            issuer: "http://aasfsdagfsd.com",
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));
        
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(tokenDescriptor);
    }
}