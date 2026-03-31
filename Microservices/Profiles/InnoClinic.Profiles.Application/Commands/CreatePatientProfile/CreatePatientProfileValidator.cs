using FluentValidation;

namespace InnoClinic.Profiles.Application.Commands.CreatePatientProfile;

public class CreatePatientProfileValidator : AbstractValidator<CreatePatientProfileCommand>
{
    public CreatePatientProfileValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Please, enter the first name"); 

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Please, enter the last name"); 

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Please, enter the phone number") 
            .Matches(@"^\+\d+$").WithMessage("You've entered an invalid phone number"); 

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Please, select the date") 
            .Must(date => date <= DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Date of birth cannot be in the future");
    }
}
