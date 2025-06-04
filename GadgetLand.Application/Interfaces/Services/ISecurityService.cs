using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Interfaces.Services;

public interface ISecurityService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
    string GenerateToken(User user);
    string GetUserIdFromToken();
    string GetEmailFromToken();
}
