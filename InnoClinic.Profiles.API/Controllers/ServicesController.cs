using InnoClinic.Profiles.Application.Services.Queries.GetServices;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace InnoClinic.Profiles.API.Controllers;
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
    public async Task<ActionResult<ServicesResponce>> GetServices([FromQuery] ServiceCategoryType categoryType = ServiceCategoryType.Consultations)
    {
        var result = await _bus.InvokeAsync<ServicesResponce>(new GetServicesQuery(categoryType));
        return Ok(result);
    }
}
