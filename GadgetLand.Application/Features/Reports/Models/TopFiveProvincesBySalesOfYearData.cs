namespace GadgetLand.Application.Features.Reports.Models;

public record TopFiveProvincesBySalesOfYearData
{
    public string Province { get; set; } = string.Empty;
    public long Sales { get; set; }
}
