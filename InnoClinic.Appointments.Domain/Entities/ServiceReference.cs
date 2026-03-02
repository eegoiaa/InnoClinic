namespace InnoClinic.Appointments.Domain.Entities;

public class ServiceReference
{
    public Guid Id { get; set; }
    public required string ServiceName { get; set; }
}
