namespace InnoClinic.Profiles.Application.Services.Queries.GetServices;

public record ServicesResponce(
    IEnumerable<ServiceGroupDto>? GroupedServices = null,
    IEnumerable<ServiceDto>? FlatServices = null
    );

