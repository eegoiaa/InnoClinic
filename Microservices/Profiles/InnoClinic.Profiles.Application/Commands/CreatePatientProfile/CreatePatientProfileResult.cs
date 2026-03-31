namespace InnoClinic.Profiles.Application.Commands.CreatePatientProfile;

public record CreatePatientProfileResult(
    bool IsMatchFound,
    Guid? ExistingProfileId = null,
    PatientDto? ExistingProfileData = null
    );
