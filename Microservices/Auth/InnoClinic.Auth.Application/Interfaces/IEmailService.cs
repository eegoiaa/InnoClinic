namespace InnoClinic.Auth.Application.Interfaces;

public interface IEmailService
{
    Task SendConfirmationLinkAsync(string email, string link);
}
