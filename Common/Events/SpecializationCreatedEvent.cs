namespace Common.Events;

public record SpecializationCreatedEvent
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}
