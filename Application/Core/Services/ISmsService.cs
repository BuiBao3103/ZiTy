namespace Application.Core.Services;

public interface ISmsService
{
    Task SendSMSAsync(string phoneNumber, string message);
}
