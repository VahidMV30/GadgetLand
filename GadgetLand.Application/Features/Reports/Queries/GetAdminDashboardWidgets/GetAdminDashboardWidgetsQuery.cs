using GadgetLand.Contracts.Reports;
using MediatR;

namespace GadgetLand.Application.Features.Reports.Queries.GetAdminDashboardWidgets;

public record GetAdminDashboardWidgetsQuery() : IRequest<AdminDashboardWidgetsResponse>;
