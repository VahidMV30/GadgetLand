using AutoMapper;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Reports;
using MediatR;
using GadgetLand.Application.Common.Extensions;

namespace GadgetLand.Application.Features.Reports.Queries.GetTopFiveProvincesBySalesOfYear;

public class GetTopFiveProvincesBySalesOfYearQueryHandler(
    IReportsRepository reportsRepository,
    IMapper mapper) : IRequestHandler<GetTopFiveProvincesBySalesOfYearQuery, IEnumerable<TopFiveProvincesBySalesOfYearResponse>>
{
    public async Task<IEnumerable<TopFiveProvincesBySalesOfYearResponse>> Handle(GetTopFiveProvincesBySalesOfYearQuery query, CancellationToken cancellationToken)
    {
        var (startOfCurrentPersianYear, startOfNextPersianYear) = query.PersianYear?.GetCurrentPersianYearRange() ??
                     DateTime.Now.GetCurrentPersianYear().GetCurrentPersianYearRange();

        var sales = await reportsRepository.GetTopFiveProvincesBySalesOfYearAsync(startOfCurrentPersianYear, startOfNextPersianYear);

        return mapper.Map<IEnumerable<TopFiveProvincesBySalesOfYearResponse>>(sales);
    }
}
