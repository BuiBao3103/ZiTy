using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using Application.Core.Services;
using Billing.Domain.Core.Repositories;
using Billing.Application.Core.Services;
using Billing.Infrastructure.Repositories;
using Billing.Infrastructure.Services;
using Billing.Infrastructure.Data;
//using Infrastructure.Services;

namespace Billing.Infrastructure;
public static class ServiceExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection services)
    {




        services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IVNPayService, VNPayService>();
        services.AddScoped<IMomoService, MomoService>();
        //services.AddScoped<ILoggerService, LoggerService>();
    }

    public static void MigrateDatabase(this IServiceProvider serviceProvider)
    {
        var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<BillingDbContext>>();

        using (var dbContext = new BillingDbContext(dbContextOptions))
        {
            dbContext.Database.Migrate();
        }
    }
}
