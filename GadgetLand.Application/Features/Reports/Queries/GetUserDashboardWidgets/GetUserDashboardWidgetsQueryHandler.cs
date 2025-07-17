using AutoMapper;
using GadgetLand.Application.Common.Extensions;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Contracts.Reports;
using MediatR;

namespace GadgetLand.Application.Features.Reports.Queries.GetUserDashboardWidgets;

public class GetUserDashboardWidgetsQueryHandler(
    ISecurityService securityService,
    IReportsRepository reportsRepository,
    IMapper mapper) : IRequestHandler<GetUserDashboardWidgetsQuery, UserDashboardWidgetsResponse>
{
    public async Task<UserDashboardWidgetsResponse> Handle(GetUserDashboardWidgetsQuery query, CancellationToken cancellationToken)
    {
        var userId = Convert.ToInt32(securityService.GetUserIdFromToken());

        var (startOfCurrentMonth, startOfNextMonth) = DateTimeExtensions.GetCurrentPersianMonthRange();

        var widgets = await reportsRepository.GetUserDashboardWidgetsAsync(userId, startOfCurrentMonth, startOfNextMonth);

        return mapper.Map<UserDashboardWidgetsResponse>(widgets);
    }
}
