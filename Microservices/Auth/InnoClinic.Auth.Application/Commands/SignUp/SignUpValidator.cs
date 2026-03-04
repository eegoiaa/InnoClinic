using FluentValidation;
using InnoClinic.Auth.Domain.Entities;

namespace InnoClinic.Auth.Application.Commands.SignUp;

public class SignUpValidator : AbstractValidator<SignUpCommand>
{
    public SignUpValidator()
    {
        RuleFor(s => s.Email)
            .NotEmpty().WithMessage("Please, enter the email")
            .EmailAddress().WithMessage("You've entered an invalid email");

        RuleFor(s => s.Password)
            .NotEmpty().WithMessage("Please, enter the password")
            .Length(6, 15).WithMessage("Password must be between 6 and 15 symbols");

        RuleFor(s => s.ConfirmPassword)
            .NotEmpty().WithMessage("Please, reenter the password")
            .Equal(x => x.Password).WithMessage("The passwords you’ve entered don’t coincide");
    }
}
