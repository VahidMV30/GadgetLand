using GadgetLand.Application.Features.Provinces.Queries.GetAllProvinces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GadgetLand.Api.Controllers;

[Route("api/[controller]")]
public class ProvincesController(IMediator mediator) : ApiController
{
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> GetAllProvinces()
    {
        var query = new GetAllProvincesQuery();

        var result = await mediator.Send(query);

        return Ok(result);
    }
}
