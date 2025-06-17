using GadgetLand.Application.Features.Settings.Queries.GetSettings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GadgetLand.Api.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController(IMediator mediator) : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetSettings()
        {
            var query = new GetSettingsQuery();

            var result = await mediator.Send(query);

            return result.Match(Ok, Problem);
        }
    }
}
