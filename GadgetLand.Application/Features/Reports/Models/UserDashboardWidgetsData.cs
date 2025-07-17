namespace GadgetLand.Application.Features.Reports.Models;

public record UserDashboardWidgetsData
{
    public int TotalOrders { get; set; }
    public long TotalPurchase { get; set; }
    public long CurrentMonthPurchase { get; set; }
    public int PendingOrders { get; set; }
    public int ProcessingOrders { get; set; }
    public int ShippedOrders { get; set; }
}
