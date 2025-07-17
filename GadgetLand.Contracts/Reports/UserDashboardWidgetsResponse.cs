namespace GadgetLand.Contracts.Reports;

public record UserDashboardWidgetsResponse
{
    public int TotalOrders { get; set; }
    public string TotalPurchase { get; set; } = string.Empty;
    public string CurrentMonthPurchase { get; set; } = string.Empty;
    public int PendingOrders { get; set; }
    public int ProcessingOrders { get; set; }
    public int ShippedOrders { get; set; }
}
