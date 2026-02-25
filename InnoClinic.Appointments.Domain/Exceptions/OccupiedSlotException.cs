using InnoClinic.Common.Exceptions;

namespace InnoClinic.Appointments.Domain.Exceptions;

public class OccupiedSlotException : BaseBusinessException
{
    public OccupiedSlotException(string message = "The doctor has already taken this time")
        : base(message)
    {
    }
}
