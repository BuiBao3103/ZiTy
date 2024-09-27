using Microsoft.EntityFrameworkCore;

namespace ZiTy.Utilities
{

    public class PaginationHandler<T>
    {
        public async Task<PaginatedResult<T>> ApplyPaginationAsync(IQueryable<T> query, int page, int pageSize)
        {
            var totalItems = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResult<T>(items, totalItems, page, pageSize);
        }
    }
}