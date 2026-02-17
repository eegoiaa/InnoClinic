using InnoClinic.Profiles.Application.Doctors.Queries.GetDoctorsList;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace InnoClinic.Profiles.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IMessageBus _messageBus;
    public DoctorsController(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DoctorsListDto>>> GetDoctors(
        [FromQuery] GetDoctorsListQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _messageBus.InvokeAsync<IEnumerable<DoctorsListDto>>(query, cancellationToken);
        return Ok(result);
    }
}
