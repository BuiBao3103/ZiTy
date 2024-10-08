using Microsoft.EntityFrameworkCore;
using zity.Data;
using zity.DTOs.Bills;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Utilities;

namespace zity.Repositories.Implementations
{
    public class BillRepository(ApplicationDbContext dbContext) : IBillRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<PaginatedResult<Bill>> GetAllAsync(BillQueryDTO queryParam)
        {
            var filterParams = new Dictionary<string, string?>
            {
                { "Id", queryParam.Id },
                { "Monthly", queryParam.Monthly },
                { "Status", queryParam.Status }
            };
            var billsQuery = _dbContext.Bills
                .Where(u => u.DeletedAt == null)
                .ApplyIncludes(queryParam.Includes)
                .ApplyFilters(filterParams)
                .ApplySorting(queryParam.Sort)
                .ApplyPaginationAsync(queryParam.Page, queryParam.PageSize);

            return await billsQuery;
        }

        public async Task<Bill?> GetByIdAsync(int id, string? includes)
        {
            var billsQuery = _dbContext.Bills.Where(u => u.DeletedAt == null)
                .ApplyIncludes(includes);
            return await billsQuery.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Bill> CreateAsync(Bill bill)
        {
            await _dbContext.Bills.AddAsync(bill);
            await _dbContext.SaveChangesAsync();
            return bill;
        }

        public async Task<Bill> UpdateAsync(Bill bill)
        {
            _dbContext.Bills.Update(bill);
            await _dbContext.SaveChangesAsync();
            return bill;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var bill = await _dbContext.Bills
                .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null);
            if (bill == null)
            {
                return false;
            }
            bill.DeletedAt = DateTime.Now;
            _dbContext.Bills.Update(bill);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        Task<PaginatedResult<Bill>> IBillRepository.GetAllAsync(BillQueryDTO queryParam)
        {
            throw new NotImplementedException();
        }
    }
}