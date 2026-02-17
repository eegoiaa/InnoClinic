namespace InnoClinic.Services.Application.Services.Queries.GetServices;

public record ServicesResponse(
    IEnumerable<ServiceGroupDto>? GroupedServices = null,
    IEnumerable<ServiceDto>? FlatServices = null
    );

