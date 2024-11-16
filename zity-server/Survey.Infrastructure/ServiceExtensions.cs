using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using Application.Core.Services;
using Survey.Domain.Core.Repositories;
using Survey.Infrastructure.Repositories;
using Survey.Infrastructure.Data;
//using Infrastructure.Services;

namespace Survey.Infrastructure;
public static class ServiceExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        //services.AddScoped<ILoggerService, LoggerService>();
    }

    public static void MigrateDatabase(this IServiceProvider serviceProvider)
    {
        var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<SurveyDbContext>>();

        using (var dbContext = new SurveyDbContext(dbContextOptions))
        {
            dbContext.Database.Migrate();
        }
    }
}
