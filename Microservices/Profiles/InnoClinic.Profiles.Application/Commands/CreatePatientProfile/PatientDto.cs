namespace InnoClinic.Profiles.Application.Commands.CreatePatientProfile;

public record PatientDto(
    Guid Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    string PhoneNumber,
    DateOnly DateOfBirth,
    string? PhotoPath
    );
