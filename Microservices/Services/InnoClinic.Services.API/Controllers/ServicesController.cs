using InnoClinic.Services.Application.Services.Queries.GetServices;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace InnoClinic.Services.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IMessageBus _bus;
    public ServicesController(IMessageBus bus)
    {
        _bus = bus;
    }

    [HttpGet]
    public async Task<ActionResult<ServicesResponse>> GetServices([FromQuery] ServiceCategoryType category = ServiceCategoryType.Consultations)
    {
        var result = await _bus.InvokeAsync<ServicesResponse>(new GetServicesQuery(category));
        return Ok(result);
    }
}
