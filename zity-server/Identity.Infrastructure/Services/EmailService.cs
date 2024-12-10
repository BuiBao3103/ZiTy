using Identity.Application.Core.Services;
using Identity.Domain.Configurations;
using Identity.Domain.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using MimeKit;

namespace Identity.Infrastructure.Services;

public class EmailService(AppSettings appSettings) : IEmailService
{
    private readonly AppSettings _appSettings = appSettings;

    public async Task SendAccountCreationEmail(User user, string password)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Zity", _appSettings.MailSettings.FromEmail));
        message.To.Add(new MailboxAddress(user.Username, user.Email));
        message.Subject = "Account Creation Notification";

        var bodyBuilder = new BodyBuilder();
        string bodyHtml = await LoadTemplateAsync("./../Identity.Application/Core/Resources/EmailTemplates/AccountCreation.html");

        bodyHtml = bodyHtml.Replace("{{FullName}}", user.FullName)
                          .Replace("{{Username}}", user.Username)
                          .Replace("{{Password}}", password)
                          .Replace("{{LoginUrl}}", _appSettings.EndpointSettings.LoginUrl);

        bodyBuilder.HtmlBody = bodyHtml;
        message.Body = bodyBuilder.ToMessageBody();

        try
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(
                _appSettings.MailSettings.Host,
                _appSettings.MailSettings.Port,
                SecureSocketOptions.StartTls
            );
            await client.AuthenticateAsync(
                _appSettings.MailSettings.Username,
                _appSettings.MailSettings.Password
            );
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
            throw new Exception("Failed to send email");
        }
    }

    private async Task<string> LoadTemplateAsync(string templatePath)
    {
        try
        {
            return await File.ReadAllTextAsync(templatePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
            throw new Exception("Failed to load email template");
        }
    }
}
