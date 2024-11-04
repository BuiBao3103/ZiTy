using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Application.Services;
using Application.Mappers;

namespace Application;
public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddScoped<IAnswerService, AnswerService>();
        services.AddScoped<IApartmentService, ApartmentService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ISettingService, SettingService>();
        services.AddScoped<IBillService, BillService>();
        services.AddScoped<IBillDetailService, BillDetailService>();
        services.AddScoped<IRejectionReasonService, RejectionReasonService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IRelationshipService, RelationshipService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddAutoMapper(
            typeof(AnswerMapping),
            typeof(ApartmentMapping)
        );
    }
}
