using InnoClinic.Auth.Application.Commands.ConfirmEmail;
using InnoClinic.Auth.Application.Commands.SignIn;
using InnoClinic.Auth.Application.Commands.SignUp;
using Microsoft.AspNetCore.Mvc;
using Wolverine;
using SignInResult = InnoClinic.Auth.Application.Commands.SignIn.SignInResult;

namespace InnoClinic.Auth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMessageBus _messageBus;
    public AuthController(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
    {
        await _messageBus.InvokeAsync(command);
        return Ok(new {message = "Success! Please check your email to confirm registration."});
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailCommand command)
    {
        await _messageBus.InvokeAsync(command);
        return Ok(new { message = "Email confirmed successfully. You can now log in." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] SignInCommand command)
    {
        var result = await _messageBus.InvokeAsync<SignInResult>(command);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(30)
        };
        Response.Cookies.Append("access_token", result.AccessToken, cookieOptions);

        var refreshCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        Response.Cookies.Append("refresh_token", result.RefreshToken, refreshCookieOptions);

        return Ok(new { Message = "You've signed in successfully" });
    }
}
