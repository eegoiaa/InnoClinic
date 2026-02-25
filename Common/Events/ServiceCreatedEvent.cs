namespace InnoClinic.Common.Events;

public record ServiceCreatedEvent
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}
