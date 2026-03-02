namespace InnoClinic.Profiles.Application.Doctors.Queries.GetDoctorsList;
public record DoctorsListDto(
    Guid Id,
    string Fullname,
    string SpecializationName,
    Guid SpecializationId,
    string OfficeAddressName,
    Guid OfficeId,
    int ExperienceYears,
    Guid? PhotoId
    );