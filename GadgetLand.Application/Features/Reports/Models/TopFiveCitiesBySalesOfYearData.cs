namespace GadgetLand.Application.Features.Reports.Models;

public record TopFiveCitiesBySalesOfYearData
{
    public string City { get; set; } = string.Empty;
    public long Sales { get; set; }
}
