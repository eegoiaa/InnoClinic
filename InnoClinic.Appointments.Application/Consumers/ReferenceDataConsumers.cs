using InnoClinic.Appointments.Domain.Entities;
using InnoClinic.Appointments.Infrastructure.Persistence;
using InnoClinic.Common.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace InnoClinic.Appointments.Application.Consumers;

public static class ReferenceDataConsumers
{
    public static async Task Handle(DoctorCreatedEvent message, AppointmentDbContext dbContext)
    {
        var exist = await dbContext.DoctorReferences.AnyAsync(d => d.Id == message.Id);
        if (exist) return;

        var doctorRef = new DoctorReference
        {
            Id = message.Id,
            FullName = message.FullName
        };

        dbContext.DoctorReferences.Add(doctorRef);
        await dbContext.SaveChangesAsync();
    }

    public static async Task Handle(ServiceCreatedEvent message, AppointmentDbContext dbContext)
    {
        var exist = await dbContext.ServiceReferences.AnyAsync(d => d.Id == message.Id);
        if (exist) return;

        var serviceRef = new ServiceReference
        {
            Id = message.Id,
            ServiceName = message.Name
        };

        dbContext.ServiceReferences.Add(serviceRef);
        await dbContext.SaveChangesAsync();
    }
}
