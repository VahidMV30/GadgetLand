using GadgetLand.Contracts.Reports;
using MediatR;

namespace GadgetLand.Application.Features.Reports.Queries.GetUserDashboardWidgets;

public record GetUserDashboardWidgetsQuery() : IRequest<UserDashboardWidgetsResponse>;
