using Application.DTOs.OtherAnswers;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class OtherAnswerMapping : Profile
    {
        public OtherAnswerMapping()
        {
            CreateMap<OtherAnswer, OtherAnswerDTO>()
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            CreateMap<OtherAnswerCreateDTO, OtherAnswer>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<OtherAnswerUpdateDTO, OtherAnswer>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<OtherAnswerPatchDTO, OtherAnswer>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Content, opt => opt.Condition((src, dest) => src.Content != null))
                .ForMember(dest => dest.QuestionId, opt => opt.Condition((src, dest) => src.QuestionId != null))
                .ForMember(dest => dest.UserId, opt => opt.Condition((src, dest) => src.UserId != null));
        }
    }
}

//public string? Content { get; set; }
//public int? QuestionId { get; set; }
//public int? UserId { get; set; }