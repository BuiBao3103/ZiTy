using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Application.Services;
using Application.Mappers;
using Application.DTOs;


namespace Application;
public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddScoped<IAnswerService, AnswerService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<ISurveyService, SurveyService>();
        services.AddScoped<IUserAnswerService, UserAnswerService>();
        services.AddScoped<IApartmentService, ApartmentService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ISettingService, SettingService>();
        services.AddAutoMapper(
            typeof(AnswerMapping),
            typeof(ApartmentMapping),
            typeof(UserMapping),
            typeof(ReportMapping),
            typeof(QuestionMapping),
            typeof(SurveyMapping),
            typeof(UserAnswerMapping)
        );
    }
}
