namespace GadgetLand.Contracts.Reports;

public record SalesByPersianMonthOfYearResponse
{
    public string PersianMonthName { get; set; } = string.Empty;
    public long Sales { get; set; }
}
