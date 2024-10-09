using Microsoft.AspNetCore.Mvc;
using zity.DTOs.Items;
using zity.Services.Interfaces;

namespace zity.Controllers
{
    public class ItemsController(IItemService itemService) : ControllerBase
    {
        private readonly IItemService _itemService = itemService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ItemQueryDTO query)
        {
            return Ok(await _itemService.GetAllAsync(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id, [FromQuery] string? includes)
        {
            var item = await _itemService.GetByIdAsync(id, includes);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ItemCreateDTO itemCreateDTO)
        {
            var createdItem = await _itemService.CreateAsync(itemCreateDTO);
            return CreatedAtAction(nameof(Get), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ItemUpdateDTO itemUpdateDTO)
        {
            var updatedItem = await _itemService.UpdateAsync(id, itemUpdateDTO);
            return updatedItem == null ? NotFound() : Ok(updatedItem);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] ItemPatchDTO itemPatchDTO)
        {
            var patchedItem = await _itemService.PatchAsync(id, itemPatchDTO);
            return patchedItem == null ? NotFound() : Ok(patchedItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _itemService.DeleteAsync(id);
            return !result ? NotFound() : NoContent();
        }
    }
}
