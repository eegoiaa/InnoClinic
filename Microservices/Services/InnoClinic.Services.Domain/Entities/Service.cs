namespace InnoClinic.Services.Domain.Entities;

public class Service
{
    public Guid Id { get; set; }
    public required string ServiceName { get; set; }
    public required decimal Price { get; set; }
    public required bool IsActive { get; set; }

    public required Guid CategoryId { get; set; }
    public ServiceCategory? Category { get; set; }

    public Guid? SpecializationId { get; set; }
}
