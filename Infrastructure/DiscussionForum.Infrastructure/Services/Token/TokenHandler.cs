using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DiscussionForum.Application.Abstractions.Token;
using DiscussionForum.Application.DTOs.Tokens;
using DiscussionForum.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DiscussionForum.Infrastructure.Services.Token;

public class TokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public JsonWebToken CreateAccessToken(User user)
    {
        JsonWebToken jsonWebToken = new();

        SecurityKey securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]!));
        
        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        jsonWebToken.Expiration = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Token:AccessTokenLifetime"]!));
        
        JwtSecurityToken jwtSecurityToken = new(
            issuer: _configuration["Token:Audience"],
            audience: _configuration["Token:Audience"], 
            expires: jsonWebToken.Expiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials,
            claims: new List<Claim>()
            {
                new(ClaimTypes.Sid, user.Id.ToString()), 
                new(ClaimTypes.Name, user.Username ?? user.Email!),
                new( ClaimTypes.Role, user.Role.ToString())
            });

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        
        jsonWebToken.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
        jsonWebToken.RefreshToken = CreateRefreshToken();

        return jsonWebToken;
    }

    public string CreateRefreshToken()
    {
        byte[] number = new byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(number);
        return Convert.ToBase64String(number);
    }
}