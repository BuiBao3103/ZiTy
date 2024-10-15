using AutoMapper;
using zity.DTOs.Reports;
using zity.Models;

namespace zity.Mappers
{
    public class ReportMapping : Profile
    {
        public ReportMapping()
        {
            CreateMap<Report, ReportDTO>()
                .ForMember(dest => dest.RejectionReasons, opt => opt.MapFrom(src => src.RejectionReasons))
                .ForMember(dest => dest.Relationship, opt => opt.MapFrom(src => src.Relationship));

            CreateMap<ReportCreateDTO, Report>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<ReportUpdateDTO, Report>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<ReportPatchDTO, Report>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
