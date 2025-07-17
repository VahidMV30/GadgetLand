using GadgetLand.Application.Features.Reports.Queries.GetAdminDashboardWidgets;
using GadgetLand.Application.Features.Reports.Queries.GetSalesByPersianMonthOfYear;
using GadgetLand.Application.Features.Reports.Queries.GetTopFiveCitiesBySalesOfYear;
using GadgetLand.Application.Features.Reports.Queries.GetTopFiveProvincesBySalesOfYear;
using GadgetLand.Application.Features.Reports.Queries.GetUserDashboardWidgets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GadgetLand.Api.Controllers;

[Route("api/[controller]")]
public class ReportsController(IMediator mediator) : ApiController
{
    [HttpGet("admin-dashboard-widgets")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAdminDashboardWidgets()
    {
        var query = new GetAdminDashboardWidgetsQuery();

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("sales-persian-month-of-year/{persianYear:int?}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetSalesByPersianMonthOfYear([FromRoute] int? persianYear)
    {
        var query = new GetSalesByPersianMonthOfYearQuery(persianYear);

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("top-five-cities-sales-year/{persianYear:int?}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetTopFiveCitiesBySalesOfYear([FromRoute] int? persianYear)
    {
        var query = new GetTopFiveCitiesBySalesOfYearQuery(persianYear);

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("top-five-provinces-sales-year/{persianYear:int?}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetTopFiveProvincesBySalesOfYear([FromRoute] int? persianYear)
    {
        var query = new GetTopFiveProvincesBySalesOfYearQuery(persianYear);

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("user-dashboard-widgets")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetUserDashboardWidgets()
    {
        var query = new GetUserDashboardWidgetsQuery();

        var result = await mediator.Send(query);

        return Ok(result);
    }
}
