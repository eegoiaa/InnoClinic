using FluentValidation;
using InnoClinic.Auth.Domain.Constants;

namespace InnoClinic.Auth.Application.Commands.SignIn;

public class SignInValidator : AbstractValidator<SignInCommand>
{
	public SignInValidator()
	{
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Please, enter the email")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("You've entered an invalid email");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Please, enter the password")
            .MinimumLength(AuthConstants.RequiredLength)
                .WithMessage($"Min {AuthConstants.RequiredLength} symbols required");
    }
}
