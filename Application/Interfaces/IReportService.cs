

using Application.DTOs;
using Application.DTOs.Reports;

namespace Application.Interfaces;

public interface IReportService
{
    Task<PaginatedResult<ReportDTO>> GetAllAsync(ReportQueryDTO query);
    Task<ReportDTO> GetByIdAsync(int id, string? includes = null);
    Task<ReportDTO> CreateAsync(ReportCreateDTO reportCreateDTO);
    Task<ReportDTO> UpdateAsync(int id, ReportUpdateDTO reportUpdateDTO);
    Task<ReportDTO> PatchAsync(int id, ReportPatchDTO reportPatchDTO);
    Task DeleteAsync(int id);
}
