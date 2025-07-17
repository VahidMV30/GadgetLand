using GadgetLand.Contracts.Reports;
using MediatR;

namespace GadgetLand.Application.Features.Reports.Queries.GetSalesByPersianMonthOfYear;

public record GetSalesByPersianMonthOfYearQuery(int? PersianYear) : IRequest<IEnumerable<SalesByPersianMonthOfYearResponse>>;
