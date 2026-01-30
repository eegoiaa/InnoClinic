namespace InnoClinic.Profiles.Application.Services.Queries.GetServices;

public record ServiceDto(
    Guid Id,
    string ServiceName,
    decimal Price
    );

