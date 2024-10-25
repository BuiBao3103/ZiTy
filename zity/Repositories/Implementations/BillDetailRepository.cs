using Microsoft.EntityFrameworkCore;
using zity.Data;
using zity.DTOs.BillDetails;
using zity.ExceptionHandling;
using zity.ExceptionHandling.Exceptions;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Utilities;

namespace zity.Repositories.Implementations
{
    public class BillDetailRepository(ApplicationDbContext dbContext) : IBillDetailRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<PaginatedResult<BillDetail>> GetAllAsync(BillDetailQueryDTO queryParam)
        {
            var filterParams = new Dictionary<string, string?>
            {
                { "Id", queryParam.Id },
                { "BillId", queryParam.BillId },
                { "ServiceId", queryParam.ServiceId }
            };

            var billDetailsQuery = _dbContext.BillDetails
                .Where(bd => bd.DeletedAt == null)
                .ApplyIncludes(queryParam.Includes)
                .ApplyFilters(filterParams)
                .ApplySorting(queryParam.Sort)
                .ApplyPaginationAsync(queryParam.Page, queryParam.PageSize);

            return await billDetailsQuery;
        }

        public async Task<BillDetail?> GetByIdAsync(int id, string? includes = null)
        {
            var billDetailQuery = _dbContext.BillDetails
                .Where(bd => bd.DeletedAt == null)
                .ApplyIncludes(includes);

            return await billDetailQuery.FirstOrDefaultAsync(bd => bd.Id == id);
        }

        public async Task<BillDetail> CreateAsync(BillDetail billDetail)
        {
            await _dbContext.BillDetails.AddAsync(billDetail);
            await _dbContext.SaveChangesAsync();
            return billDetail;
        }

        public async Task<BillDetail> UpdateAsync(BillDetail billDetail)
        {
            _dbContext.BillDetails.Update(billDetail);
            await _dbContext.SaveChangesAsync();
            return billDetail;
        }

        public async Task DeleteAsync(int id)
        {
            var billDetail = await _dbContext.BillDetails
                .FirstOrDefaultAsync(bd => bd.Id == id && bd.DeletedAt == null)
                ?? throw new EntityNotFoundException(nameof(BillDetail), id);
            billDetail.DeletedAt = DateTime.Now;
            _dbContext.BillDetails.Update(billDetail);
            await _dbContext.SaveChangesAsync();
        }
    }
}
