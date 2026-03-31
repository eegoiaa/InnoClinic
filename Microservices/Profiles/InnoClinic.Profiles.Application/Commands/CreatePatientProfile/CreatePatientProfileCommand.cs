namespace InnoClinic.Profiles.Application.Commands.CreatePatientProfile;

public record CreatePatientProfileCommand(
    string FirstName,
    string LastName,
    string? MiddleName,
    string PhoneNumber,
    DateOnly DateOfBirth,
    string? PhotoPath,
    bool BypassMatching = false
    );

