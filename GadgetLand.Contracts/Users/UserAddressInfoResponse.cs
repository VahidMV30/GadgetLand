namespace GadgetLand.Contracts.Users;

public record UserAddressInfoResponse
{
    public int? ProvinceId { get; set; }
    public string? ProvinceName { get; set; }
    public int? CityId { get; set; }
    public string? CityName { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Mobile { get; set; }
    public string? PostalCode { get; set; }
    public string? Address { get; set; }
}
