using AutoMapper;
using zity.DTOs.OtherAnswers;
using zity.Models;

namespace zity.Mappers
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
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}

