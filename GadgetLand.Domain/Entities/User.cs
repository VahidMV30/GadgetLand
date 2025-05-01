namespace GadgetLand.Domain.Entities;

public class User
{
    public int Id { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;

    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
