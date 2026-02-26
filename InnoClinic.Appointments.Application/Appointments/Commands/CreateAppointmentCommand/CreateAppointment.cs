using System.Security.Cryptography.X509Certificates;

namespace InnoClinic.Appointments.Application.Appointments.Commands.CreateAppointmentCommand;

public record CreateAppointment(
    Guid PatientId,
    Guid DoctorId,
    Guid ServiceId,
    DateOnly Date,
    TimeOnly Time
    );

