using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using Application.Core.Services;
using Domain.Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using MyApp.Domain.Configurations;
//using Infrastructure.Services;

namespace Infrastructure;
public static class ServiceExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection services)
    {
        DotNetEnv.Env.Load();
        var appSettings = new AppSettings
        {
            mySqlSettings = new MySqlSettings
            {
                Server = Environment.GetEnvironmentVariable("MYSQL_SERVER") ?? throw new ArgumentException("MYSQL_SERVER is missing."),
                Port = int.Parse(Environment.GetEnvironmentVariable("MYSQL_PORT") ?? throw new ArgumentException("MYSQL_PORT is missing.")),
                Database = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? throw new ArgumentException("MYSQL_DATABASE is missing."),
                User = Environment.GetEnvironmentVariable("MYSQL_USER") ?? throw new ArgumentException("MYSQL_USER is missing."),
                Password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? throw new ArgumentException("MYSQL_PASSWORD is missing.")
            }
        };

        var connectionString = appSettings.mySqlSettings.GetConnectionString();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //services.AddScoped<IEmailService, EmailService>();
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
