using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GadgetLand.Infrastructure.Services;

public class SecurityService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : ISecurityService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.GivenName, user.FullName),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.Name),
            new("FullName", user.FullName)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: claims,
            signingCredentials: credentials,
            expires: DateTime.UtcNow.AddDays(configuration.GetValue<int>("JwtSettings:ExpiryInDays"))
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GetEmailFromToken()
    {
        return httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email)!;
    }
}
