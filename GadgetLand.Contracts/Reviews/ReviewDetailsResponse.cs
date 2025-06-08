namespace GadgetLand.Contracts.Reviews;

public record ReviewDetailsResponse
{
    public int Id { get; set; }
    public string UserFullName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public double Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public bool IsConfirmed { get; set; }
    public string CreatedAt { get; set; } = string.Empty;
}
