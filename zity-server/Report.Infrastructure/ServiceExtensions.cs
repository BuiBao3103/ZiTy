using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using Application.Core.Services;
using Report.Domain.Core.Repositories;
using Report.Infrastructure.Repositories;
using Report.Infrastructure.Data;
//using Infrastructure.Services;

namespace Report.Infrastructure;
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
        var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<ReportDbContext>>();

        using (var dbContext = new ReportDbContext(dbContextOptions))
        {
            dbContext.Database.Migrate();
        }
    }
}
