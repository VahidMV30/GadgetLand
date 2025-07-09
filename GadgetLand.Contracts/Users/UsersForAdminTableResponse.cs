namespace GadgetLand.Contracts.Users;

public record UsersForAdminTableResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string RegisterDate { get; set; } = string.Empty;
}
