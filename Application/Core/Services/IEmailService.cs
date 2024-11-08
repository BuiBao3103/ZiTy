using Domain.Entities;
namespace Application.Core.Services;

public interface IEmailService
{
    Task SendAccountCreationEmail(User user, string password);
}
