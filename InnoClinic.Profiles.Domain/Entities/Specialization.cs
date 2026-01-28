namespace InnoClinic.Profiles.Domain.Entities;

public class Specialization
{
    public Guid Id { get; set; }
    public string? SpecializationName { get; set; }
    public bool IsActive { get; set; }
}
