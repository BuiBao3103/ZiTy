using Microsoft.EntityFrameworkCore;
using zity.Data;
using zity.DTOs.RejectionReasons;
using zity.ExceptionHandling;
using zity.ExceptionHandling.Exceptions;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Utilities;

namespace zity.Repositories.Implementations
{
    public class RejectionReasonRepository(ApplicationDbContext dbContext) : IRejectionReasonRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<PaginatedResult<RejectionReason>> GetAllAsync(RejectionReasonQueryDTO queryParam)
        {
            var filterParams = new Dictionary<string, string?>
                {
                    { "Id", queryParam.Id },
                    { "Content", queryParam.Content },
                    { "ReportId", queryParam.ReportId?.ToString() }
                };
            var rejectionReasonsQuery = _dbContext.RejectionReasons
                .Where(u => u.DeletedAt == null)
                .ApplyIncludes(queryParam.Includes)
                .ApplyFilters(filterParams)
                .ApplySorting(queryParam.Sort)
                .ApplyPaginationAsync(queryParam.Page, queryParam.PageSize);

            return await rejectionReasonsQuery;
        }

        public async Task<RejectionReason?> GetByIdAsync(int id, string? includes)
        {
            var rejectionReasonsQuery = _dbContext.RejectionReasons.Where(u => u.DeletedAt == null)
                .ApplyIncludes(includes);
            return await rejectionReasonsQuery.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<RejectionReason> CreateAsync(RejectionReason rejectionReason)
        {
            await _dbContext.RejectionReasons.AddAsync(rejectionReason);
            await _dbContext.SaveChangesAsync();
            return rejectionReason;
        }

        public async Task<RejectionReason> UpdateAsync(RejectionReason rejectionReason)
        {
            _dbContext.RejectionReasons.Update(rejectionReason);
            await _dbContext.SaveChangesAsync();
            return rejectionReason;
        }


        public async Task DeleteAsync(int id)
        {
            var rejectionReason = await _dbContext.RejectionReasons
                .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null)
                                ?? throw new EntityNotFoundException(nameof(RejectionReason), id);

            rejectionReason.DeletedAt = DateTime.Now;
            _dbContext.RejectionReasons.Update(rejectionReason);
            await _dbContext.SaveChangesAsync();
        }
    }
}
