using Common.Events;
using InnoClinic.Services.Domain.Entities;
using InnoClinic.Services.Infrastructure.Persistence;

namespace InnoClinic.Services.Application.Events;

public static class SpecializationCreatedConsumer
{
    public static async Task Handle(
        SpecializationCreatedEvent message,
        ServicesDbContext dbContext
        )
    {
        var lookup = new SpecializationReference { Id = message.Id, Name = message.Name };
        dbContext.SpecializationReferences.Add( lookup );
        await dbContext.SaveChangesAsync();
    } 
}
