using zity.DTOs.Items;
using zity.Models;

namespace zity.Mappers
{
    public class ItemMapper
    {
        // ToDTO
        public static ItemDTO ItemToDTO(Item item) =>
            new ItemDTO
            {
                Id = item.Id,
                Image = item.Image,
                Description = item.Description,
                IsReceive = item.IsReceive,
                UserId = item.UserId,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt,
                //User = item.User != null ? UserMapping.ToDTO(item.User) : null,
            };

        // ToModelFromCreate
        public static Item ToModelFromCreate(ItemCreateDTO itemCreateDTO) =>
            new Item
            {
                Image = itemCreateDTO.Image,
                Description = itemCreateDTO.Description,
                IsReceive = itemCreateDTO.IsReceive,
                UserId = itemCreateDTO.UserId,
                CreatedAt = DateTime.Now,
            };

        // UpdateModelFromUpdate
        public static Item UpdateModelFromUpdate(Item item, ItemUpdateDTO updateDTO)
        {
            item.Image = updateDTO.Image;
            item.Description = updateDTO.Description;
            item.IsReceive = updateDTO.IsReceive;
            item.UserId = updateDTO.UserId;
            item.UpdatedAt = DateTime.Now;
            return item;
        }

        // PatchModelFromPatch
        public static Item PatchModelFromPatch(Item item, ItemPatchDTO patchDTO)
        {
            if (patchDTO.Image != null)
                item.Image = patchDTO.Image;
            if (patchDTO.Description != null)
                item.Description = patchDTO.Description;
            if (patchDTO.IsReceive.HasValue)
                item.IsReceive = patchDTO.IsReceive.Value;
            if (patchDTO.UserId.HasValue)
                item.UserId = patchDTO.UserId.Value;
            item.UpdatedAt = DateTime.Now;
            return item;
        }

    }
}
