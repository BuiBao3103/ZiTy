using zity.DTOs.Reports;
using zity.Models;
using zity.Utilities;

namespace zity.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<PaginatedResult<Report>> GetAllAsync(ReportQueryDTO query);
        Task<Report?> GetByIdAsync(int id, string? includes = null);
        Task<Report> CreateAsync(Report report);
        Task<Report> UpdateAsync(Report report);
        Task DeleteAsync(int id);
    }
}
