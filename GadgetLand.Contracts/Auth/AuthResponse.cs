namespace GadgetLand.Contracts.Auth;

public record AuthResponse
{
    public int Id { get; set; }
    public string Role { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
