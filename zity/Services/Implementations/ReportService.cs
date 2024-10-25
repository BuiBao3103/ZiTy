using AutoMapper;
using zity.DTOs.Reports;
using zity.ExceptionHandling.Exceptions;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class ReportService(IReportRepository reportRepository, IMapper mapper) : IReportService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IReportRepository _reportRepository = reportRepository;

        public async Task<PaginatedResult<ReportDTO>> GetAllAsync(ReportQueryDTO queryParam)
        {
            var pageReports = await _reportRepository.GetAllAsync(queryParam);
            var reports = pageReports.Contents.Select(_mapper.Map<ReportDTO>).ToList();

            return new PaginatedResult<ReportDTO>(
                reports,
                pageReports.TotalItems,
                pageReports.Page,
                pageReports.PageSize);
        }

        public async Task<ReportDTO> GetByIdAsync(int id, string? includes)
        {
            var report = await _reportRepository.GetByIdAsync(id, includes)
                    ?? throw new EntityNotFoundException(nameof(Report), id);
            return _mapper.Map<ReportDTO>(report);
        }

        public async Task<ReportDTO> CreateAsync(ReportCreateDTO createDTO)
        {
            var report = _mapper.Map<Report>(createDTO);
            return _mapper.Map<ReportDTO>(await _reportRepository.CreateAsync(report));
        }

        public async Task<ReportDTO> UpdateAsync(int id, ReportUpdateDTO updateDTO)
        {
            var existingReport = await _reportRepository.GetByIdAsync(id)
                    ?? throw new EntityNotFoundException(nameof(Report), id);
            _mapper.Map(updateDTO, existingReport);
            var updatedReport = await _reportRepository.UpdateAsync(existingReport);
            return _mapper.Map<ReportDTO>(updatedReport);
        }

        public async Task<ReportDTO> PatchAsync(int id, ReportPatchDTO patchDTO)
        {
            var existingReport = await _reportRepository.GetByIdAsync(id)
                    ?? throw new EntityNotFoundException(nameof(Report), id);
            _mapper.Map(patchDTO, existingReport);
            var patchedReport = await _reportRepository.UpdateAsync(existingReport);
            return _mapper.Map<ReportDTO>(patchedReport);
        }

        public async Task DeleteAsync(int id)
        {
            await _reportRepository.DeleteAsync(id);
        }
    }
}
