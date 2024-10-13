using zity.DTOs.Items;
using zity.Mappers;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class ItemService(IItemRepository itemRepository) : IItemService
    {
        private readonly IItemRepository _itemRepository = itemRepository;

        public async Task<PaginatedResult<ItemDTO>> GetAllAsync(ItemQueryDTO queryParam)
        {
            var pageItems = await _itemRepository.GetAllAsync(queryParam);
            return new PaginatedResult<ItemDTO>(
                pageItems.Contents.Select(ItemMapper.ItemToDTO).ToList(),
                pageItems.TotalItems,
                pageItems.Page,
                pageItems.PageSize);
        }


        public async Task<ItemDTO?> GetByIdAsync(int id, string? includes)
        {
            var item = await _itemRepository.GetByIdAsync(id, includes);
            return item != null ? ItemMapper.ItemToDTO(item) : null;
        }
        public async Task<ItemDTO> CreateAsync(ItemCreateDTO createDTO)
        {
            var item = ItemMapper.ToModelFromCreate(createDTO);
            return ItemMapper.ItemToDTO(await _itemRepository.CreateAsync(item));
        }
        public async Task<ItemDTO?> UpdateAsync(int id, ItemUpdateDTO updateDTO)
        {
            var existingItem = await _itemRepository.GetByIdAsync(id, null);
            if (existingItem == null)
            {
                return null;
            }

            ItemMapper.UpdateModelFromUpdate(existingItem, updateDTO);
            var updatedItem = await _itemRepository.UpdateAsync(existingItem);
            return ItemMapper.ItemToDTO(updatedItem);
        }

        public async Task<ItemDTO?> PatchAsync(int id, ItemPatchDTO patchDTO)
        {
            var existingItem = await _itemRepository.GetByIdAsync(id, null);
            if (existingItem == null)
            {
                return null;
            }

            ItemMapper.PatchModelFromPatch(existingItem, patchDTO);
            var patchedItem = await _itemRepository.UpdateAsync(existingItem);
            return ItemMapper.ItemToDTO(patchedItem);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _itemRepository.DeleteAsync(id);
        }
    }
}
