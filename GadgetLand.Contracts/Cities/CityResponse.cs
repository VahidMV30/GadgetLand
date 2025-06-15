namespace GadgetLand.Contracts.Cities;

public record CityResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
