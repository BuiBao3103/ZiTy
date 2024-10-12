using Microsoft.EntityFrameworkCore;
using zity.Data;
using zity.DTOs.Items;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Utilities;

namespace zity.Repositories.Implementations
{
    public class ItemRepository: IItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedResult<Item>> GetAllAsync(ItemQueryDTO queryParam)
        {
            var filterParams = new Dictionary<string, string?>
                {
                    { "Id", queryParam.Id },
                    { "UserId", queryParam.UserId },
                };
            var itemsQuery = _dbContext.Items
                .Where(u => u.DeletedAt == null)
                .ApplyIncludes(queryParam.Includes)
                .ApplyFilters(filterParams)
                .ApplySorting(queryParam.Sort)
                .ApplyPaginationAsync(queryParam.Page, queryParam.PageSize);

            return await itemsQuery;
        }

        public async Task<Item?> GetByIdAsync(int id, string? includes)
        {
            var itemsQuery = _dbContext.Items.Where(u => u.DeletedAt == null)
                .ApplyIncludes(includes);
            return await itemsQuery.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Item> CreateAsync(Item item)
        {
            await _dbContext.Items.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<Item> UpdateAsync(Item item)
        {
            _dbContext.Items.Update(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _dbContext.Items
                .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null);
            if (item == null)
            {
                return false;
            }
            item.DeletedAt = DateTime.Now;
            _dbContext.Items.Update(item);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
