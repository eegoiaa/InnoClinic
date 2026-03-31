namespace InnoClinic.Profiles.Domain.Entities;

public class Patient
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public required string PhoneNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }

    public Guid? AccountId { get; set; }
    public bool IsLinkedToAccount { get; set; }
    public string? PhotoPath { get; set; }
}