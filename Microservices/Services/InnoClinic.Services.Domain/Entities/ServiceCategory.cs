namespace InnoClinic.Services.Domain.Entities;

public class ServiceCategory
{
    public Guid Id { get; set; }
    public required string CategoryName { get; set; }
    public required int TimeSlotSize { get; set; }
    public ICollection<Service> Services { get; set; } = new List<Service>();
}
