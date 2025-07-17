namespace GadgetLand.Contracts.Reports;

public record TopFiveCitiesBySalesOfYearResponse
{
    public string City { get; set; } = string.Empty;
    public long Sales { get; set; }
}
