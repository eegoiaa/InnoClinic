using InnoClinic.Appointments.Domain.Entities;
using InnoClinic.Appointments.Infrastructure.Persistence;
using InnoClinic.Common.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace InnoClinic.Appointments.Application.Consumers;

public static class ReferenceDataConsumers
{
    public static async Task Handle(DoctorCreatedEvent message, AppointmentDbContext dbContext, CancellationToken cancellationToken)
    {
        var exist = await dbContext.DoctorReferences.AnyAsync(d => d.Id == message.Id, cancellationToken);
        if (exist) return;

        var doctorRef = new DoctorReference
        {
            Id = message.Id,
            FullName = message.FullName
        };

        await dbContext.DoctorReferences.AddAsync(doctorRef, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public static async Task Handle(ServiceCreatedEvent message, AppointmentDbContext dbContext, CancellationToken cancellationToken)
    {
        var exist = await dbContext.ServiceReferences.AnyAsync(d => d.Id == message.Id, cancellationToken);
        if (exist) return;

        var serviceRef = new ServiceReference
        {
            Id = message.Id,
            ServiceName = message.Name
        };

        await dbContext.ServiceReferences.AddAsync(serviceRef, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
