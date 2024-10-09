using Microsoft.EntityFrameworkCore;
using zity.Data;
using zity.DTOs.Reports;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Utilities;

namespace zity.Repositories.Implementations
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ReportRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedResult<Report>> GetAllAsync(ReportQueryDTO queryParam)
        {
            var filterParams = new Dictionary<string, string?>
                {
                    { "Id", queryParam.Id },
                    { "RelationshipId", queryParam.RelationshipId }
                };
            var reportsQuery = _dbContext.Reports
                .Where(u => u.DeletedAt == null)
                .ApplyIncludes(queryParam.Includes)
                .ApplyFilters(filterParams)
                .ApplySorting(queryParam.Sort)
                .ApplyPaginationAsync(queryParam.Page, queryParam.PageSize);

            return await reportsQuery;
        }

        public async Task<Report?> GetByIdAsync(int id, string? includes)
        {
            var reportsQuery = _dbContext.Reports.Where(u => u.DeletedAt == null)
                .ApplyIncludes(includes);
            return await reportsQuery.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Report> CreateAsync(Report report)
        {
            await _dbContext.Reports.AddAsync(report);
            await _dbContext.SaveChangesAsync();
            return report;
        }

        public async Task<Report> UpdateAsync(Report report)
        {
            _dbContext.Reports.Update(report);
            await _dbContext.SaveChangesAsync();
            return report;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var report = await _dbContext.Reports
                .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null);
            if (report == null)
            {
                return false;
            }
            report.DeletedAt = DateTime.Now;
            _dbContext.Reports.Update(report);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
