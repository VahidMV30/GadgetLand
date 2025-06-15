using GadgetLand.Application.Features.Cities.Queries.GetCitiesByProvinceId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GadgetLand.Api.Controllers;

[Route("api/[controller]")]
public class CitiesController(IMediator mediator) : ApiController
{
    [HttpGet("cities-by-province-id/{provinceId:int}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> GetCitiesByProvinceId(int provinceId)
    {
        var query = new GetCitiesByProvinceIdQuery(provinceId);

        var result = await mediator.Send(query);

        return Ok(result);
    }
}
