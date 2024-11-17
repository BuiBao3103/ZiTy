using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using Application.Core.Services;
using Apartment.Domain.Core.Repositories;
using Apartment.Application.Core.Services;
using Apartment.Infrastructure.Repositories;
using Apartment.Infrastructure.Services;
using Apartment.Infrastructure.Data;
//using Infrastructure.Services;

namespace Apartment.Infrastructure;
public static class ServiceExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection services)
    {




        services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IMediaService, MediaService>();
        //services.AddScoped<ILoggerService, LoggerService>();
    }

    public static void MigrateDatabase(this IServiceProvider serviceProvider)
    {
        var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<ApartmentDbContext>>();

        using (var dbContext = new ApartmentDbContext(dbContextOptions))
        {
            dbContext.Database.Migrate();
        }
    }
}
