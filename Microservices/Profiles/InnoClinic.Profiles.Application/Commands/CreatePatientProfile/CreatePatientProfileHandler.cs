using InnoClinic.Profiles.Domain.Entities;
using InnoClinic.Profiles.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InnoClinic.Profiles.Application.Commands.CreatePatientProfile;

public static class CreatePatientProfileHandler
{
    public static async Task<CreatePatientProfileResult> Handle(
        CreatePatientProfileCommand command,
        ProfileDbContext dbContext,
        ClaimsPrincipal user
        )
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userId, out var accountId))
            throw new UnauthorizedAccessException("User identification failed.");

        if (!command.BypassMatching)
        {
            var match = await dbContext.Patients
                .Where(p => !p.IsLinkedToAccount)
                .Select(p => new
                {
                    Patient = p,
                    Score = (p.FirstName == command.FirstName ? 5 : 0) +
                            (p.LastName == command.LastName ? 5 : 0) +
                            (p.MiddleName == command.MiddleName ? 5 : 0) +
                            (p.DateOfBirth == command.DateOfBirth ? 3 : 0)
                })
                .Where(x => x.Score >= 13)
                .OrderByDescending(x => x.Score)
                .FirstOrDefaultAsync();

            if (match != null)
            {
                return new CreatePatientProfileResult(
                    IsMatchFound: true,
                    ExistingProfileId: match.Patient.Id,
                    ExistingProfileData: new PatientDto(
                        match.Patient.Id,
                        match.Patient.FirstName,
                        match.Patient.LastName,
                        match.Patient.MiddleName,
                        match.Patient.PhoneNumber,
                        match.Patient.DateOfBirth,
                        match.Patient.PhotoPath
                    )
                );
            }
        }

        var newPatient = new Patient
        {
            Id = Guid.NewGuid(),
            FirstName = command.FirstName,
            LastName = command.LastName,
            MiddleName = command.MiddleName,
            PhoneNumber = command.PhoneNumber,
            DateOfBirth = command.DateOfBirth,
            PhotoPath = command.PhotoPath,
            AccountId = accountId, 
            IsLinkedToAccount = true
        };

        dbContext.Patients.Add(newPatient);
        await dbContext.SaveChangesAsync();

        return new CreatePatientProfileResult(IsMatchFound: false);
    }
}
