using AutoMapper;
using Survey.Domain.Entities;
using Survey.Application.DTOs.Surveys;

namespace Survey.Application.Mappers;

public class SurveyMapping : Profile
{
    public SurveyMapping()
    {
        CreateMap<Survey.Domain.Entities.Survey, SurveyDTO>()
            .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));

        CreateMap<SurveyCreateDTO, Survey.Domain.Entities.Survey>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<SurveyUpdateDTO, Survey.Domain.Entities.Survey>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<SurveyPatchDTO, Survey.Domain.Entities.Survey>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Title, opt => opt.Condition((src, dest) => src.Title != null))
            .ForMember(dest => dest.StartDate, opt => opt.Condition((src, dest) => src.StartDate != null))
            .ForMember(dest => dest.EndDate, opt => opt.Condition((src, dest) => src.EndDate != null))
            .ForMember(dest => dest.TotalQuestions, opt => opt.Condition((src, dest) => src.TotalQuestions != null))
            .ForMember(dest => dest.UserCreateId, opt => opt.Condition((src, dest) => src.UserCreateId != null));

        CreateMap<SurveyCreateFullDTO, Survey.Domain.Entities.Survey>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions.Select(q => new Question
            {
                CreatedAt = DateTime.Now,
                Content = q.Content,
                Answers = q.Answers.Select(a => new Answer
                {
                    CreatedAt = DateTime.Now,
                    Content = a.Content
                }).ToList()
            }).ToList()));
    }
}

