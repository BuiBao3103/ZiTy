using Application.DTOs.Surveys;
using AutoMapper;
using Domain.Core.Models;
using Domain.Entities;
using System.Diagnostics.Contracts;

namespace Application.Mappers;

public class SurveyMapping : Profile
{
    public SurveyMapping()
    {
        CreateMap<Survey, SurveyDTO>()
            .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions))
            .ForMember(dest => dest.UserCreate, opt => opt.MapFrom(src => src.UserCreate));

        CreateMap<SurveyCreateDTO, Survey>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<SurveyUpdateDTO, Survey>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<SurveyPatchDTO, Survey>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Title, opt => opt.Condition((src, dest) => src.Title != null))
            .ForMember(dest => dest.StartDate, opt => opt.Condition((src, dest) => src.StartDate != null))
            .ForMember(dest => dest.EndDate, opt => opt.Condition((src, dest) => src.EndDate != null))
            .ForMember(dest => dest.TotalQuestions, opt => opt.Condition((src, dest) => src.TotalQuestions != null))
            .ForMember(dest => dest.UserCreateId, opt => opt.Condition((src, dest) => src.UserCreateId != null));

        CreateMap<SurveyCreateFullDTO, Survey>()
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
        CreateMap<QuestionStatistics, QuestionStatisticsDto>()
                .ForMember(dest => dest.Answers, opt =>
                    opt.MapFrom(src => src.Answers))
                .ForMember(dest => dest.OtherAnswers, opt =>
                    opt.MapFrom(src => src.OtherAnswers));

        CreateMap<QuestionStatistics, AnswerDetailDto>()
            .ForMember(dest => dest.IsMostSelected, opt => opt.MapFrom(src => false));

        CreateMap<OtherAnswerStatistics, OtherAnswerDetailDto>();
    }
}

