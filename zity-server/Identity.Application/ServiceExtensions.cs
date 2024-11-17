using Microsoft.Extensions.DependencyInjection;
using Identity.Application.Interfaces;
using Identity.Application.Mappers;
using Identity.Application.Services;


namespace Identity.Application;
public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
     
        services.AddScoped<IUserService, UserService>();
       
        services.AddScoped<IAuthService, AuthService>();
        services.AddAutoMapper(
            typeof(UserMapping)
        );
    }
}
