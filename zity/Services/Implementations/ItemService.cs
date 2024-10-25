using AutoMapper;
using zity.DTOs.Items;
using zity.ExceptionHandling.Exceptions;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ItemDTO>> GetAllAsync(ItemQueryDTO queryParam)
        {
            var pageItems = await _itemRepository.GetAllAsync(queryParam);
            var items = pageItems.Contents.Select(_mapper.Map<ItemDTO>).ToList();
            return new PaginatedResult<ItemDTO>(
                items,
                pageItems.TotalItems,
                pageItems.Page,
                pageItems.PageSize);
        }

        public async Task<ItemDTO> GetByIdAsync(int id, string? includes)
        {
            var item = await _itemRepository.GetByIdAsync(id, includes)
                    ?? throw new EntityNotFoundException(nameof(Item), id);
            return _mapper.Map<ItemDTO>(item);
        }

        public async Task<ItemDTO> CreateAsync(ItemCreateDTO createDTO)
        {
            var item = _mapper.Map<Item>(createDTO);
            return _mapper.Map<ItemDTO>(await _itemRepository.CreateAsync(item));
        }

        public async Task<ItemDTO> UpdateAsync(int id, ItemUpdateDTO updateDTO)
        {
            var existingItem = await _itemRepository.GetByIdAsync(id)
                    ?? throw new EntityNotFoundException(nameof(Item), id);
            _mapper.Map(updateDTO, existingItem);
            var updatedItem = await _itemRepository.UpdateAsync(existingItem);
            return _mapper.Map<ItemDTO>(updatedItem);
        }

        public async Task<ItemDTO> PatchAsync(int id, ItemPatchDTO patchDTO)
        {
            var existingItem = await _itemRepository.GetByIdAsync(id)
                    ?? throw new EntityNotFoundException(nameof(Item), id);
            _mapper.Map(patchDTO, existingItem);
            var patchedItem = await _itemRepository.UpdateAsync(existingItem);
            return _mapper.Map<ItemDTO>(patchedItem);
        }

        public async Task DeleteAsync(int id)
        {
            await _itemRepository.DeleteAsync(id);
        }
    }
}
