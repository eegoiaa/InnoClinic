using FluentValidation;

namespace InnoClinic.Appointments.Application.Appointments.Commands.CreateAppointmentCommand;

public class CreateAppointmentValidator : AbstractValidator<CreateAppointment>
{
	public CreateAppointmentValidator()
	{
		RuleFor(a => a.PatientId).NotEmpty();
		RuleFor(a => a.DoctorId).NotEmpty();
		RuleFor(a => a.ServiceId).NotEmpty();
        RuleFor(x => x.Date).GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("You can't sign up for a past date");
    }
}
