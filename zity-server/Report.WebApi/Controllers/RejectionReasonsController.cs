using Report.Application.DTOs.RejectionReasons;
using Report.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Report.WebApi.Controllers;

[Route("api/rejectionReasons")]
[ApiController]

public class RejectionReasonsController(IRejectionReasonService rejectionReasonService) : ControllerBase
{
    private readonly IRejectionReasonService _rejectionReasonService = rejectionReasonService;
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] RejectionReasonQueryDTO query)
    {
        return Ok(await _rejectionReasonService.GetAllAsync(query));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, [FromQuery] string? includes)
    {
        return Ok(await _rejectionReasonService.GetByIdAsync(id, includes));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RejectionReasonCreateDTO rejectionReasonCreateDTO)
    {
        var createdRejectionReason = await _rejectionReasonService.CreateAsync(rejectionReasonCreateDTO);
        return CreatedAtAction(nameof(Get), new { id = createdRejectionReason.Id }, createdRejectionReason);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RejectionReasonUpdateDTO rejectionReasonUpdateDTO)
    {
        return Ok(await _rejectionReasonService.UpdateAsync(id, rejectionReasonUpdateDTO));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] RejectionReasonPatchDTO rejectionReasonPatchDTO)
    {
        return Ok(await _rejectionReasonService.PatchAsync(id, rejectionReasonPatchDTO));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _rejectionReasonService.DeleteAsync(id);
        return NoContent();
    }
}
