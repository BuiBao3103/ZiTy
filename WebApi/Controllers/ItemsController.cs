using Application.DTOs.Items;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("api/items")]
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
        return Ok(await _itemService.GetByIdAsync(id, includes));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ItemCreateDTO itemCreateDTO)
    {
        var createdAnswer = await _itemService.CreateAsync(itemCreateDTO);
        return CreatedAtAction(nameof(Get), new { id = createdAnswer.Id }, createdAnswer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ItemUpdateDTO itemUpdateDTO)
    {
        return Ok(await _itemService.UpdateAsync(id, itemUpdateDTO));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] ItemPatchDTO itemPatchDTO)
    {
        return Ok(await _itemService.PatchAsync(id, itemPatchDTO));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _itemService.DeleteAsync(id);
        return NoContent();
    }
}

