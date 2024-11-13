using Microsoft.Extensions.DependencyInjection;
using Survey.Application.Interfaces;
using Survey.Application.Services;
using Survey.Application.Mappers;


namespace Survey.Application;
public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddScoped<IAnswerService, AnswerService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<ISurveyService, SurveyService>();
        services.AddScoped<IUserAnswerService, UserAnswerService>();
        services.AddScoped<IOtherAnswerService, OtherAnswerService>();
        services.AddAutoMapper(
            typeof(AnswerMapping),
            typeof(QuestionMapping),
            typeof(SurveyMapping),
            typeof(UserAnswerMapping),
            typeof(OtherAnswerMapping)
        );
    }
}
