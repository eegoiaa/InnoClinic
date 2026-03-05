using InnoClinic.Auth.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace InnoClinic.Auth.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendConfirmationLinkAsync(string email, string link)
    {
        var host = _configuration["SmtpSettings:Host"];
        var port = int.Parse(_configuration["SmtpSettings:Port"] ?? "2525");
        var username = _configuration["SmtpSettings:Username"];
        var password = _configuration["SmtpSettings:Password"];
        var fromEmail = _configuration["SmtpSettings:FromEmail"];

        using var client = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(username, password),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(fromEmail!),
            Subject = "InnoClinic - Confirm your registration",
            Body = $"<h1>Welcome!</h1><p>Please confirm your registration by clicking <a href='{link}'>this link</a>.</p>",
            IsBodyHtml = true
        };
        mailMessage.To.Add(email);

        await client.SendMailAsync(mailMessage);
    }
}
