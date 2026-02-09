using InnoClinic.Profiles.Application.Specializations.Commands.CreateSpecialization;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace InnoClinic.Profiles.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecializationController : ControllerBase
{
    private readonly IMessageBus _bus;
    public SpecializationController(IMessageBus bus)
    {
        _bus = bus;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSpecialization([FromBody] CreateSpecializationCommand command, CancellationToken cancellationToken)
    {
        await _bus.InvokeAsync(command, cancellationToken);
        return Ok();
    }
}
