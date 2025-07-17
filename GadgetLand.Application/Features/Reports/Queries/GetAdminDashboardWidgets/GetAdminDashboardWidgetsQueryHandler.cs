using AutoMapper;
using GadgetLand.Application.Common.Extensions;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Reports;
using MediatR;

namespace GadgetLand.Application.Features.Reports.Queries.GetAdminDashboardWidgets;

public class GetAdminDashboardWidgetsQueryHandler(
    IReportsRepository reportsRepository,
    IMapper mapper) : IRequestHandler<GetAdminDashboardWidgetsQuery, AdminDashboardWidgetsResponse>
{
    public async Task<AdminDashboardWidgetsResponse> Handle(GetAdminDashboardWidgetsQuery query, CancellationToken cancellationToken)
    {
        var (startOfCurrentMonth, startOfNextMonth) = DateTimeExtensions.GetCurrentPersianMonthRange();
        var (startOfToday, startOfTomorrow) = DateTimeExtensions.GetTodayRange();

        var widgetsData = await reportsRepository.GetAdminDashboardWidgetsAsync(startOfCurrentMonth, startOfNextMonth, startOfToday, startOfTomorrow);

        return mapper.Map(widgetsData, new AdminDashboardWidgetsResponse());
    }
}
