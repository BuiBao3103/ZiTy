using Apartment.Application.DTOs;
using Apartment.Application.DTOs.Items;
using Microsoft.AspNetCore.Http;

namespace Apartment.Application.Interfaces;

public interface IItemService
{
    Task<PaginatedResult<ItemDTO>> GetAllAsync(ItemQueryDTO query);
    Task<ItemDTO> GetByIdAsync(int id, string? includes = null);
    Task<ItemDTO> CreateAsync(ItemCreateDTO itemCreateDTO);
    Task<ItemDTO> UpdateAsync(int id, ItemUpdateDTO itemUpdateDTO);
    Task<ItemDTO> PatchAsync(int id, ItemPatchDTO itemPatchDTO);
    Task DeleteAsync(int id);
    Task<ItemDTO> UploadImageAsync(int id, IFormFile file);
}

