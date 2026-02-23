namespace InnoClinic.Appointments.Domain.Entities;

public class DoctorReference
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
}
