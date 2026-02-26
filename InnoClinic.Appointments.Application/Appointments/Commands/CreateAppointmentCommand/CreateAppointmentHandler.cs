using InnoClinic.Appointments.Domain.Entities;
using InnoClinic.Appointments.Domain.Exceptions;
using InnoClinic.Appointments.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Appointments.Application.Appointments.Commands.CreateAppointmentCommand;

public static class CreateAppointmentHandler
{
    public static async Task Handle(
        CreateAppointment command,
        AppointmentDbContext dbContext, 
        CancellationToken cancellationToken)
    {
        var doctorExist = await dbContext.DoctorReferences
            .AnyAsync(d => d.Id == command.DoctorId, cancellationToken);

        if (!doctorExist)
            throw new NotFoundException($"Doctor with ID {command.DoctorId} not found.");

        var serviceExist = await dbContext.ServiceReferences
            .AnyAsync(s => s.Id == command.ServiceId, cancellationToken);

        if (!serviceExist)
            throw new NotFoundException($"Service with ID {command.ServiceId} not found.");

        var isBusy = await dbContext.Appointments
            .AnyAsync(a => a.DoctorId == command.DoctorId &&
                      a.Date == command.Date &&
                      a.Time == command.Time, 
                      cancellationToken);

        if (isBusy)
            throw new OccupiedSlotException();

        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            PatientId = command.PatientId,
            DoctorId = command.DoctorId,
            ServiceId = command.ServiceId,
            Date = command.Date,
            Time = command.Time,
            IsApproved = false
        };

        dbContext.Appointments.Add(appointment);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
