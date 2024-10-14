using AutoMapper;
using zity.DTOs.Surveys;
using zity.Models;

namespace zity.Mappers
{
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
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
