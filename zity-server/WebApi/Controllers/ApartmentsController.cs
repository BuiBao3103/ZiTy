using Application.DTOs.Apartments;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/apartments")]
[ApiController]
public class ApartmentsController(IApartmentService apartmentService) : ControllerBase
{
    private readonly IApartmentService _apartmentService = apartmentService;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ApartmentQueryDTO query)
    {
        return Ok(await _apartmentService.GetAllAsync(query));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id, [FromQuery] string? includes)
    {
        var relationship = await _apartmentService.GetByIdAsync(id, includes);
        return relationship == null ? NotFound() : Ok(relationship);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ApartmentCreateDTO apartmentCreateDTO)
    {
        var createdApartment = await _apartmentService.CreateAsync(apartmentCreateDTO);
        return CreatedAtAction(nameof(Get), new { id = createdApartment.Id }, createdApartment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] ApartmentUpdateDTO apartmentUpdateDTO)
    {
        var updatedApartment = await _apartmentService.UpdateAsync(id, apartmentUpdateDTO);
        return updatedApartment == null ? NotFound() : Ok(updatedApartment);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch([FromRoute] string id, [FromBody] ApartmentPatchDTO apartmentPatchDTO)
    {
        var patchedApartment = await _apartmentService.PatchAsync(id, apartmentPatchDTO);
        return patchedApartment == null ? NotFound() : Ok(patchedApartment);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        await _apartmentService.DeleteAsync(id);
        return NoContent();
    }
}
