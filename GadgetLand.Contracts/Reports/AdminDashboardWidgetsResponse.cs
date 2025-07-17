namespace GadgetLand.Contracts.Reports;

public record AdminDashboardWidgetsResponse
{
    public int ProductsCount { get; set; }
    public int UsersCount { get; set; }
    public int OrdersCount { get; set; }
    public string TotalSales { get; set; } = string.Empty;
    public string CurrentMonthSales { get; set; } = string.Empty;
    public string TodaySales { get; set; } = string.Empty;
}
