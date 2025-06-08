namespace GadgetLand.Contracts.Reviews;

public record ReviewResponse
{
    public int Id { get; set; }
    public string UserFullName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public double Rating { get; set; }
    public bool IsConfirmed { get; set; }
    public string CreatedAt { get; set; } = string.Empty;
}
