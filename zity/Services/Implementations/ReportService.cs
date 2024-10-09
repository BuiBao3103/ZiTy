using zity.DTOs.Reports;
using zity.Mappers;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class ReportService(IReportRepository reportRepository) : IReportService
    {
        private readonly IReportRepository _reportRepository = reportRepository;

        public async Task<PaginatedResult<ReportDTO>> GetAllAsync(ReportQueryDTO queryParam)
        {
            var pageReports = await _reportRepository.GetAllAsync(queryParam);
            return new PaginatedResult<ReportDTO>(
                pageReports.Contents.Select(ReportMapper.ReportToDTO).ToList(),
                pageReports.TotalItems,
                pageReports.Page,
                pageReports.PageSize);
        }


        public async Task<ReportDTO?> GetByIdAsync(int id, string? includes)
        {
            var report = await _reportRepository.GetByIdAsync(id, includes);
            return report != null ? ReportMapper.ReportToDTO(report) : null;
        }
        public async Task<ReportDTO> CreateAsync(ReportCreateDTO createDTO)
        {
            var report = ReportMapper.ToModelFromCreate(createDTO);
            return ReportMapper.ReportToDTO(await _reportRepository.CreateAsync(report));
        }
        public async Task<ReportDTO?> UpdateAsync(int id, ReportUpdateDTO updateDTO)
        {
            var existingReport = await _reportRepository.GetByIdAsync(id, null);
            if (existingReport == null)
            {
                return null;
            }

            ReportMapper.UpdateModelFromUpdate(existingReport, updateDTO);
            var updatedReport = await _reportRepository.UpdateAsync(existingReport);
            return ReportMapper.ReportToDTO(updatedReport);
        }

        public async Task<ReportDTO?> PatchAsync(int id, ReportPatchDTO patchDTO)
        {
            var existingReport = await _reportRepository.GetByIdAsync(id, null);
            if (existingReport == null)
            {
                return null;
            }

            ReportMapper.PatchModelFromPatch(existingReport, patchDTO);
            var patchedReport = await _reportRepository.UpdateAsync(existingReport);
            return ReportMapper.ReportToDTO(patchedReport);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _reportRepository.DeleteAsync(id);
        }
    }
}
