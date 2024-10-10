using zity.Models;

namespace zity.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendAccountCreationEmail(User user, string password);
    }
}
