using AutoMapper;
using GadgetLand.Application.Common.Extensions;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Reports;
using MediatR;

namespace GadgetLand.Application.Features.Reports.Queries.GetTopFiveCitiesBySalesOfYear;

public class GetTopFiveCitiesBySalesOfYearQueryHandler(
    IReportsRepository reportsRepository,
    IMapper mapper) : IRequestHandler<GetTopFiveCitiesBySalesOfYearQuery, IEnumerable<TopFiveCitiesBySalesOfYearResponse>>
{
    public async Task<IEnumerable<TopFiveCitiesBySalesOfYearResponse>> Handle(GetTopFiveCitiesBySalesOfYearQuery query, CancellationToken cancellationToken)
    {
        var (startOfCurrentPersianYear, startOfNextPersianYear) = query.PersianYear?.GetCurrentPersianYearRange() ??
                                                                  DateTime.Now.GetCurrentPersianYear().GetCurrentPersianYearRange();

        var sales = await reportsRepository.GetTopFiveCitiesBySalesOfYearAsync(startOfCurrentPersianYear, startOfNextPersianYear);

        return mapper.Map<IEnumerable<TopFiveCitiesBySalesOfYearResponse>>(sales);
    }
}
