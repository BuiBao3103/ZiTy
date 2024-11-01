using AutoMapper;
using zity.DTOs.Answers;
using zity.DTOs.RejectionReasons;
using zity.Models;
using zity.Utilities;

namespace zity.Mappers
{
    public class RejectionReasonMapping : Profile
    {
        public RejectionReasonMapping()
        {
            CreateMap<RejectionReason, RejectionReasonDTO>()
               .ForMember(dest => dest.Report, opt => opt.MapFrom(src => src.Report));

            CreateMap<RejectionReasonCreateDTO, RejectionReason>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<RejectionReasonUpdateDTO, RejectionReason>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<RejectionReasonPatchDTO, RejectionReason>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
