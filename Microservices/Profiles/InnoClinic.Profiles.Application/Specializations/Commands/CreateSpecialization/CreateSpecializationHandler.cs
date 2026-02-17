using Common.Events;
using InnoClinic.Profiles.Domain.Entities;
using InnoClinic.Profiles.Infrastructure.Persistence;
using Wolverine;

namespace InnoClinic.Profiles.Application.Specializations.Commands.CreateSpecialization;

public static class CreateSpecializationHandler
{
    public static async Task Handle(
        CreateSpecializationCommand command,
        ProfileDbContext dbContext,
        IMessageBus bus
        )
    {
        var specialization = new Specialization { SpecializationName = command.Name };
        dbContext.Specializations.Add(specialization);
        await dbContext.SaveChangesAsync();

        await bus.PublishAsync(new SpecializationCreatedEvent { Id = specialization.Id, Name = specialization.SpecializationName});
    }
}
