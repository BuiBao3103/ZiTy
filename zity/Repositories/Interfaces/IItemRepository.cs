using zity.DTOs.Items;
using zity.Models;
using zity.Utilities;

namespace zity.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<PaginatedResult<Item>> GetAllAsync(ItemQueryDTO query);
        Task<Item?> GetByIdAsync(int id, string? includes);
        Task<Item> CreateAsync(Item item);
        Task<Item> UpdateAsync(Item item);
        Task<bool> DeleteAsync(int id);
    }
}
