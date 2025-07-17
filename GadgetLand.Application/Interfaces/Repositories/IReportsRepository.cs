using GadgetLand.Application.Features.Reports.Models;

namespace GadgetLand.Application.Interfaces.Repositories;

public interface IReportsRepository
{
    Task<AdminDashboardWidgetsData> GetAdminDashboardWidgetsAsync(DateTime startOfCurrentMonth, DateTime startOfNextMonth, DateTime startOfToday, DateTime startOfTomorrow);
    Task<IEnumerable<SalesByPersianMonthOfYearData>> GetSalesByPersianMonthOfYearAsync(DateTime startOfCurrentPersianYear, DateTime startOfNextPersianYear);
    Task<IEnumerable<TopFiveCitiesBySalesOfYearData>> GetTopFiveCitiesBySalesOfYearAsync(DateTime startOfCurrentPersianYear, DateTime startOfNextPersianYear);
    Task<IEnumerable<TopFiveProvincesBySalesOfYearData>> GetTopFiveProvincesBySalesOfYearAsync(DateTime startOfCurrentPersianYear, DateTime startOfNextPersianYear);
    Task<UserDashboardWidgetsData> GetUserDashboardWidgetsAsync(int userId, DateTime startOfCurrentMonth, DateTime startOfNextMonth);
}
