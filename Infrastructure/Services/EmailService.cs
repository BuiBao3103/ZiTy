using Application.Core.Services;
using Domain.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MyApp.Domain.Configurations;

namespace Infrastructure.Services;

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
        string bodyHtml = await LoadTemplateAsync("../Application/Core/Resources/EmailTemplates/AccountCreation.html");
        bodyHtml = bodyHtml.Replace("{{FullName}}", user.FullName)
                           .Replace("{{Username}}", user.Username)
                           .Replace("{{Password}}", password)
                           .Replace("{{LoginUrl}}", _appSettings.EndpointSettings.LoginUrl);
        bodyBuilder.HtmlBody = bodyHtml;

        message.Body = bodyBuilder.ToMessageBody();

        try
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(_appSettings.MailSettings.Host, _appSettings.MailSettings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_appSettings.MailSettings.Username, _appSettings.MailSettings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            //throw new AppError($"Failed to send email: {ex.Message}", StatusCodes.Status500InternalServerError, "SEND_EMAIL_FAILED");
        }
    }

    private static async Task<string> LoadTemplateAsync(string templatePath)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), templatePath);
        return await File.ReadAllTextAsync(filePath);
    }
}
