using Apartment.Application.DTOs.Relationships;
using Apartment.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apartment.WebApi.Controllers;

[Route("api/relationships")]
[ApiController]
public class RelationshipsController(IRelationshipService relationshipService) : ControllerBase
{
    private readonly IRelationshipService _relationshipService = relationshipService;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] RelationshipQueryDTO query)
    {
        return Ok(await _relationshipService.GetAllAsync(query));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, [FromQuery] string? includes)
    {
        return Ok(await _relationshipService.GetByIdAsync(id, includes));
    }

    //Authorize role only for cladmin


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RelationshipCreateDTO relationshipCreateDTO)
    {
        var createdRelationship = await _relationshipService.CreateAsync(relationshipCreateDTO);
        return CreatedAtAction(nameof(Get), new { id = createdRelationship.Id }, createdRelationship);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RelationshipUpdateDTO relationshipUpdateDTO)
    {
        return Ok(await _relationshipService.UpdateAsync(id, relationshipUpdateDTO));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] RelationshipPatchDTO relationshipPatchDTO)
    {
        return Ok(await _relationshipService.PatchAsync(id, relationshipPatchDTO));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _relationshipService.DeleteAsync(id);
        return NoContent();
    }
}
