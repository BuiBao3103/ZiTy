using AutoMapper;
using zity.DTOs.Questions;
using zity.Models;
using zity.Utilities;

namespace zity.Mappers
{
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
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
