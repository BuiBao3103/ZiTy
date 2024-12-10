using Identity.Domain.Entities;
namespace Identity.Application.Core.Services;

public interface IEmailService
{
    Task SendAccountCreationEmail(User user, string password);
}
