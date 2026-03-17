namespace InnoClinic.Auth.Domain.Constants;

public static class RolesConstants
{
    public const string Admin = "Admin";
    public const string Doctor = "Doctor";
    public const string Patient = "Patient";
    public const string Receptionist = "Receptionist";

    public static readonly Guid AdminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    public static readonly Guid DoctorId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    public static readonly Guid PatientId = Guid.Parse("33333333-3333-3333-3333-333333333333");
    public static readonly Guid ReceptionistId = Guid.Parse("44444444-4444-4444-4444-444444444444");
}
