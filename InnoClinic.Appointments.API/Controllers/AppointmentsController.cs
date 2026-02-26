using InnoClinic.Appointments.Application.Appointments.Commands.CreateAppointmentCommand;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace InnoClinic.Appointments.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IMessageBus _messageBus;

    public AppointmentsController(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateAppointment command, CancellationToken cancellationToken)
    {
        await _messageBus.InvokeAsync(command, cancellationToken);
        return Ok(new { message = "Appointment has been created" });
    }
}
