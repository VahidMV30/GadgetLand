using Dapper;
using GadgetLand.Application.Features.Reports.Models;
using GadgetLand.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GadgetLand.Infrastructure.Persistence.Repositories;

public class ReportsRepository(GadgetLandDbContext dbContext) : IReportsRepository
{
    public async Task<AdminDashboardWidgetsData> GetAdminDashboardWidgetsAsync(
        DateTime startOfCurrentMonth,
        DateTime startOfNextMonth,
        DateTime startOfToday,
        DateTime startOfTomorrow)
    {
        await using var dbConnection = dbContext.Database.GetDbConnection();

        const string query = "dbo.AdminDashboardWidgets";

        await using var multiQuery = await dbConnection.QueryMultipleAsync(query, new
        {
            @StartOfCurrentMonth = startOfCurrentMonth,
            @StartOfNextMonth = startOfNextMonth,
            @StartOfToday = startOfToday,
            @StartOfTomorrow = startOfTomorrow
        });

        return new AdminDashboardWidgetsData
        {
            ProductsCount = await multiQuery.ReadFirstAsync<int>(),
            UsersCount = await multiQuery.ReadFirstAsync<int>(),
            OrdersCount = await multiQuery.ReadFirstAsync<int>(),
            TotalSales = await multiQuery.ReadFirstAsync<long>(),
            CurrentMonthSales = await multiQuery.ReadFirstAsync<long>(),
            TodaySales = await multiQuery.ReadFirstAsync<long>()
        };
    }

    public async Task<IEnumerable<SalesByPersianMonthOfYearData>> GetSalesByPersianMonthOfYearAsync(
        DateTime startOfCurrentPersianYear, DateTime startOfNextPersianYear)
    {
        await using var dbConnection = dbContext.Database.GetDbConnection();

        const string query = "dbo.SalesByPersianMonthOfYear";

        var result = await dbConnection.QueryAsync<SalesByPersianMonthOfYearData>(query, new
        {
            @StartOfCurrentPersianYear = startOfCurrentPersianYear,
            @StartOfNextPersianYear = startOfNextPersianYear,
        });

        return result;
    }

    public async Task<IEnumerable<TopFiveCitiesBySalesOfYearData>> GetTopFiveCitiesBySalesOfYearAsync(DateTime startOfCurrentPersianYear, DateTime startOfNextPersianYear)
    {
        await using var dbConnection = dbContext.Database.GetDbConnection();

        const string query = "dbo.TopFiveCitiesBySalesOfYear";

        var result = await dbConnection.QueryAsync<TopFiveCitiesBySalesOfYearData>(query, new
        {
            @StartOfCurrentPersianYear = startOfCurrentPersianYear,
            @StartOfNextPersianYear = startOfNextPersianYear,
        });

        return result;
    }

    public async Task<IEnumerable<TopFiveProvincesBySalesOfYearData>> GetTopFiveProvincesBySalesOfYearAsync(DateTime startOfCurrentPersianYear, DateTime startOfNextPersianYear)
    {
        await using var dbConnection = dbContext.Database.GetDbConnection();

        const string query = "dbo.TopFiveProvincesBySalesOfYear";

        var result = await dbConnection.QueryAsync<TopFiveProvincesBySalesOfYearData>(query, new
        {
            @StartOfCurrentPersianYear = startOfCurrentPersianYear,
            @StartOfNextPersianYear = startOfNextPersianYear,
        });

        return result;
    }

    public async Task<UserDashboardWidgetsData> GetUserDashboardWidgetsAsync(int userId, DateTime startOfCurrentMonth, DateTime startOfNextMonth)
    {
        await using var dbConnection = dbContext.Database.GetDbConnection();

        const string query = "dbo.UserDashboardWidgets";

        var multiQuery = await dbConnection.QueryMultipleAsync(query, new
        {
            @UserId = userId,
            @StartOfCurrentMonth = startOfCurrentMonth,
            @StartOfNextMonth = startOfNextMonth
        });

        return new UserDashboardWidgetsData
        {
            TotalOrders = await multiQuery.ReadFirstAsync<int>(),
            TotalPurchase = await multiQuery.ReadFirstAsync<long>(),
            CurrentMonthPurchase = await multiQuery.ReadFirstAsync<long>(),
            PendingOrders = await multiQuery.ReadFirstAsync<int>(),
            ProcessingOrders = await multiQuery.ReadFirstAsync<int>(),
            ShippedOrders = await multiQuery.ReadFirstAsync<int>()
        };
    }
}
