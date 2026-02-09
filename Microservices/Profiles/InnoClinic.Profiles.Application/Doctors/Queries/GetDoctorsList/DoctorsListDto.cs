namespace InnoClinic.Profiles.Application.Doctors.Queries.GetDoctorsList;
public record DoctorsListDto(
    Guid Id,
    string Fullname,
    string SpecializationName,
    string OfficeAddressName,
    int ExperienceYears,
    Guid? PhotoId
    );