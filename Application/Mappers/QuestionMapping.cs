using Application.DTOs.Questions;
using AutoMapper;
using Domain.Entities;


namespace Application.Mappers;

public class QuestionMapping : Profile
{
    public QuestionMapping()
    {
        CreateMap<Question, QuestionDTO>()
          .ForMember(dest => dest.Survey, opt => opt.MapFrom(src => src.Survey))
          .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers))
          .ForMember(dest => dest.OtherAnswers, opt => opt.MapFrom(src => src.OtherAnswers));

        CreateMap<QuestionCreateDTO, Question>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<QuestionUpdateDTO, Question>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<QuestionPatchDTO, Question>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Content, opt => opt.Condition((src, dest) => src.Content != null))
            .ForMember(dest => dest.SurveyId, opt => opt.Condition((src, dest) => src.SurveyId != null));
    }
}
