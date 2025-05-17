using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IdentityX.BusinessLogic.Models;
using IdentityX.BusinessLogic.ServiceContracts;
using IdentityX.DataAccess.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace IdentityX.BusinessLogic.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public AuthenticationModel CreateJwtToken(JwtModel jwtModel)
    {
        DateTime expiration = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));
        Claim[] claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, jwtModel.RelationshipId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
            new Claim(ClaimTypes.Name, jwtModel.PersonName),
            new Claim(ClaimTypes.Email, jwtModel.Email),
            new Claim(ClaimTypes.NameIdentifier, jwtModel.PersonName),
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken tokenGenerator = new JwtSecurityToken(

            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: expiration,
            signingCredentials: credentials
        );
        
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        string token = tokenHandler.WriteToken(tokenGenerator);
        
        return new AuthenticationModel() 
        { Token = token, 
            Email = jwtModel.Email, 
            PersonName = jwtModel.PersonName, 
            Expiration = expiration,
            RefreshToken = GenerateRefreshToken(),
            RefreshTokenExpirationDateTime = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["RefreshToken:EXPIRATION_MINUTES"]))
        };
    }
    
    private string GenerateRefreshToken()
    {
        byte[] bytes = new byte[64];
        var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
}