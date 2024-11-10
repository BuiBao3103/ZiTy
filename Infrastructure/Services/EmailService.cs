using Application.Core.Services;
using Domain.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
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

        // Replace placeholders with actual values including base64 images
        bodyHtml = bodyHtml.Replace("{{FullName}}", user.FullName)
                          .Replace("{{Username}}", user.Username)
                          .Replace("{{Password}}", password)
                          .Replace("{{LoginUrl}}", _appSettings.EndpointSettings.LoginUrl)
                          .Replace("YOUR_HOSTED_LOGO_URL", EmailImageResources.LogoBase64)
                          .Replace("YOUR_HOSTED_FIREWORK_URL", EmailImageResources.FireworkBase64)
                          .Replace("YOUR_HOSTED_FOOTER_LOGO_URL", EmailImageResources.FooterLogoBase64);

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

public static class EmailImageResources
{
    public static readonly string LogoBase64 = "data:image/svg+xml;base64," + Convert.ToBase64String(File.ReadAllBytes("../Application/Core/Resources/Images/Zity-logo-256x256px.svg"));
    public static readonly string FireworkBase64 = "data:image/svg+xml;base64," + Convert.ToBase64String(File.ReadAllBytes("../Application/Core/Resources/Images/Firework.svg"));
    public static readonly string FooterLogoBase64 = "data:image/svg+xml;base64," + Convert.ToBase64String(File.ReadAllBytes("../Application/Core/Resources/Images/Zity-logo-128x128px.svg"));
}
