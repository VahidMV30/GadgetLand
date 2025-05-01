using GadgetLand.Application.Features.Auth.Commands.Login;
using GadgetLand.Application.Features.Auth.Commands.Register;
using GadgetLand.Application.Features.Auth.Queries.GetProfile;
using GadgetLand.Contracts.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GadgetLand.Api.Controllers;

[Route("api/[controller]")]
public class AuthController(IMediator mediator, IWebHostEnvironment webHostEnvironment, IConfiguration configuration) : ApiController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.FullName, request.Email, request.Password, request.ConfirmPassword);

        var result = await mediator.Send(command);

        return result.Match(
            response =>
            {
                Response.Cookies.Append("token", response.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = webHostEnvironment.IsProduction(),
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("JwtSettings:ExpiryInDays"))
                });

                return Ok(response.AuthResponse);
            }, Problem);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = new LoginCommand(request.Email, request.Password);

        var result = await mediator.Send(command);

        return result.Match(
            response =>
            {
                Response.Cookies.Append("token", response.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = webHostEnvironment.IsProduction(),
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("JwtSettings:ExpiryInDays"))
                });

                return Ok(response.AuthResponse);
            }, Problem);
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> Profile()
    {
        var query = new GetProfileQuery();

        var result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("token");

        return Ok();
    }
}
