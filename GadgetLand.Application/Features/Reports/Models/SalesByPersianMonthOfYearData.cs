namespace GadgetLand.Application.Features.Reports.Models;

public record SalesByPersianMonthOfYearData
{
    public string PersianMonthName { get; set; } = string.Empty;
    public long Sales { get; set; }
}
