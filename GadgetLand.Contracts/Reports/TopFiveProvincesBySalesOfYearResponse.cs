namespace GadgetLand.Contracts.Reports;

public record TopFiveProvincesBySalesOfYearResponse
{
    public string Province { get; set; } = string.Empty;
    public long Sales { get; set; }
}
