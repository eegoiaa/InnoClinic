using InnoClinic.Appointments.Domain.Exceptions;
using InnoClinic.Common.Exceptions;
using InnoClinic.Profiles.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InnoClinic.Profiles.Application.Commands.LinkExistingProfile;
public static class LinkExistingProfileHandler
{
    public static async Task Handle(
        LinkExistingProfileCommand command,
        ProfileDbContext dbContext,
        ClaimsPrincipal user
        )
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userId, out var accountId))
            throw new UnauthorizedAccessException("User identification failed.");

        var patient = await dbContext.Patients
            .FirstOrDefaultAsync(p => p.Id == command.ProfileId) ?? throw new NotFoundException("Profile not found.");
        
        if (patient.IsLinkedToAccount)
            throw new ConflictException("This profile is already linked to another account.");

        patient.AccountId = accountId;
        patient.IsLinkedToAccount = true;

        await dbContext.SaveChangesAsync();
    }
}

