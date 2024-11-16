using Microsoft.Extensions.DependencyInjection;
using Report.Application.Interfaces;
using Report.Application.Mappers;
using Report.Application.Services;


namespace Report.Application;
public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddScoped<IReportService, ReportService>();
    
        services.AddScoped<IRejectionReasonService, RejectionReasonService>();
  
        services.AddAutoMapper(
            typeof(ReportMapping),
            typeof(RejectionReasonMapping)
        );
    }
}
