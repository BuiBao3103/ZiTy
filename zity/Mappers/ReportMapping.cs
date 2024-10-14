using AutoMapper;
using zity.DTOs.Reports;
using zity.Models;

namespace zity.Mappers
{
    public class ReportMapping : Profile
    {
        public ReportMapping()
        {
            CreateMap<Report, ReportDTO>();

            CreateMap<ReportCreateDTO, Report>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<ReportUpdateDTO, Report>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<ReportPatchDTO, Report>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
