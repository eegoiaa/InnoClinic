namespace InnoClinic.Profiles.Application.Doctors.Queries.GetDoctorsList;

public record GetDoctorsListQuery(
    string? SearchByName = null,
    Guid? SpecializationId = null,
    Guid? OfficeId = null
);

//we use null because the form can be submitted without any field by condition
