using Microsoft.Extensions.DependencyInjection;
using Apartment.Application.Interfaces;
using Apartment.Application.Services;
using Apartment.Application.Mappers;


namespace Apartment.Application;
public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddScoped<IApartmentService, ApartmentService>();
        services.AddScoped<IRelationshipService, RelationshipService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddAutoMapper(
            typeof(ApartmentMapping),
            typeof(ItemMapping),
            typeof(RelationshipMapping)
        );
    }
}
