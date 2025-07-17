using GadgetLand.Contracts.Reports;
using MediatR;

namespace GadgetLand.Application.Features.Reports.Queries.GetTopFiveProvincesBySalesOfYear;

public record GetTopFiveProvincesBySalesOfYearQuery(int? PersianYear) : IRequest<IEnumerable<TopFiveProvincesBySalesOfYearResponse>>;
