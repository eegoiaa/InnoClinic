namespace InnoClinic.Profiles.Application.Services.Queries.GetServices;

public record ServiceGroupDto(
    string SpecializationName,
    IEnumerable<ServiceDto> Services
    );

