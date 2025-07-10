namespace GadgetLand.Contracts.Users;

public record UserDetailsResponse
{
    public int Id { get; set; }
    public int? ProvinceId { get; set; }
    public string Province { get; set; } = string.Empty;
    public int? CityId { get; set; }
    public string City { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string RegisterDate { get; set; } = string.Empty;
}
