using Microsoft.Extensions.DependencyInjection;
using Billing.Application.Services;
using Billing.Application.Interfaces;
using Billing.Application.Mappers;


namespace Billing.Application;
public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {

        services.AddScoped<ISettingService, SettingService>();
        services.AddScoped<IBillService, BillService>();
        services.AddScoped<IBillDetailService, BillDetailService>();
        services.AddScoped<IServiceService, ServiceService>();

        services.AddAutoMapper(
            typeof(BillDetailMapping),
            typeof(BillMapping),
            typeof(ServiceMapping),
            typeof(SettingMapping)
        );
    }
}
