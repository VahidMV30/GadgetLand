namespace GadgetLand.Domain.Entities;

public class User
{
    public int Id { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;

    public int? CityId { get; set; }
    public City? City { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Mobile { get; set; } = string.Empty;
    public string? PostalCode { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;

    public ICollection<Review> Reviews { get; set; } = [];
    public ICollection<Payment> Payments { get; set; } = [];
    public ICollection<Order> Orders { get; set; } = [];
}
