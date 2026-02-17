namespace InnoClinic.Services.Application.Services.Queries.GetServices;

public record ServiceGroupDto(
    string Specialization,
    IEnumerable<ServiceDto> Services
    );

