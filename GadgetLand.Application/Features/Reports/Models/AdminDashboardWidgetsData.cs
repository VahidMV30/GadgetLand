namespace GadgetLand.Application.Features.Reports.Models;

public record AdminDashboardWidgetsData
{
    public int ProductsCount { get; set; }
    public int UsersCount { get; set; }
    public int OrdersCount { get; set; }
    public long TotalSales { get; set; }
    public long CurrentMonthSales { get; set; }
    public long TodaySales { get; set; }
}
