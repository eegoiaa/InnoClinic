namespace InnoClinic.Common.Events;

public record DoctorCreatedEvent
{
    public required Guid Id { get; init; }
    public required string FullName { get; init; }
}
