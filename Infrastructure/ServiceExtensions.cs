using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using Application.Core.Services;
using Domain.Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using MyApp.Domain.Configurations;
using Infrastructure.Services;
using Application.Core.Services;
//using Infrastructure.Services;

namespace Infrastructure;
public static class ServiceExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection services)
    {
        

        

        services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IMediaService, MediaService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IVNPayService, VNPayService>();
        services.AddScoped<IMomoService, MomoService>();
        services.AddScoped<ISmsService, SmsService>();
        //services.AddScoped<ILoggerService, LoggerService>();
    }

    public static void MigrateDatabase(this IServiceProvider serviceProvider)
    {
        var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();

        using (var dbContext = new ApplicationDbContext(dbContextOptions))
        {
            dbContext.Database.Migrate();
        }
    }
}
