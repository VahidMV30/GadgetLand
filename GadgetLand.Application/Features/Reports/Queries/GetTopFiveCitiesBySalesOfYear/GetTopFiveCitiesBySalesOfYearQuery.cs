using GadgetLand.Contracts.Reports;
using MediatR;

namespace GadgetLand.Application.Features.Reports.Queries.GetTopFiveCitiesBySalesOfYear;

public record GetTopFiveCitiesBySalesOfYearQuery(int? PersianYear) : IRequest<IEnumerable<TopFiveCitiesBySalesOfYearResponse>>;
