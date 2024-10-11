namespace zity.Services.Interfaces
{
    public interface ISmsService
    {
        Task SendSMSAsync(string phoneNumber, string message);
    }
}
