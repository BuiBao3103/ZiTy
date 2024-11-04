using Application.DTOs;
using Application.DTOs.Items;

namespace Application.Interfaces;

public interface IItemService
{
    Task<PaginatedResult<ItemDTO>> GetAllAsync(ItemQueryDTO query);
    Task<ItemDTO> GetByIdAsync(int id, string? includes = null);
    Task<ItemDTO> CreateAsync(ItemCreateDTO itemCreateDTO);
    Task<ItemDTO> UpdateAsync(int id, ItemUpdateDTO itemUpdateDTO);
    Task<ItemDTO> PatchAsync(int id, ItemPatchDTO itemPatchDTO);
    Task DeleteAsync(int id);
}

