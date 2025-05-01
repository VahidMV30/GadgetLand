using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace GadgetLand.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        var firstError = errors.FirstOrDefault();

        return firstError.Type switch
        {
            ErrorType.Validation => BadRequest(new { errors = errors.Select(e => new { e.Code, e.Description }) }),
            ErrorType.Conflict => Conflict(new { errors = errors.Select(e => new { e.Code, e.Description }) }),
            ErrorType.Failure => BadRequest(new { errors = errors.Select(e => new { e.Code, e.Description }) }),
            ErrorType.NotFound => NotFound(new { errors = errors.Select(e => new { e.Code, e.Description }) }),
            _ => StatusCode(500, new { errors = errors.Select(e => new { e.Code, e.Description }) })
        };
    }
}
