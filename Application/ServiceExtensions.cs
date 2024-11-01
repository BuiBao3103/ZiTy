﻿using Microsoft.Extensions.DependencyInjection;
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
        services.AddAutoMapper(
            typeof(AnswerMapping),
            typeof(ApartmentMapping)
        );
    }
}