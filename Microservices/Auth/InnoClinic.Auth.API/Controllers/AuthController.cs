using InnoClinic.Auth.Application.Commands.ConfirmEmail;
using InnoClinic.Auth.Application.Commands.SignUp;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

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
}
