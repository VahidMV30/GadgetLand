using AutoMapper;
using GadgetLand.Application.Common.Extensions;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Contracts.Reports;
using MediatR;

namespace GadgetLand.Application.Features.Reports.Queries.GetSalesByPersianMonthOfYear;

public class GetSalesByPersianMonthOfYearQueryHandler(
    IReportsRepository reportsRepository,
    IMapper mapper) : IRequestHandler<GetSalesByPersianMonthOfYearQuery, IEnumerable<SalesByPersianMonthOfYearResponse>>
{
    public async Task<IEnumerable<SalesByPersianMonthOfYearResponse>> Handle(GetSalesByPersianMonthOfYearQuery query, CancellationToken cancellationToken)
    {
        var (startOfCurrentPersianYear, startOfNextPersianYear) = query.PersianYear?.GetCurrentPersianYearRange() ?? DateTime.Now.GetCurrentPersianYear().GetCurrentPersianYearRange();

        var sales = await reportsRepository.GetSalesByPersianMonthOfYearAsync(startOfCurrentPersianYear, startOfNextPersianYear);

        return mapper.Map<IEnumerable<SalesByPersianMonthOfYearResponse>>(sales);
    }
}
