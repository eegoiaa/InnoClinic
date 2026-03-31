using InnoClinic.Profiles.Application.Commands.CreatePatientProfile;
using InnoClinic.Profiles.Application.Commands.LinkExistingProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace InnoClinic.Profiles.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PatientController : ControllerBase
{
    private readonly IMessageBus _bus;

    public PatientController(IMessageBus bus)
    {
        _bus = bus;
    }

    [HttpPost]
    public async Task<ActionResult<CreatePatientProfileResult>> CreatePatientProfile(
        [FromBody] CreatePatientProfileCommand command,
        CancellationToken ct)
    {
        var result = await _bus.InvokeAsync<CreatePatientProfileResult>(command, ct);
        return Ok(result);
    }

    [HttpPost("link")]
    public async Task<IActionResult> LinkExistingProfile(
        [FromBody] LinkExistingProfileCommand command,
        CancellationToken ct)
    {
        await _bus.InvokeAsync(command, ct);
        return Ok();
    }
}
