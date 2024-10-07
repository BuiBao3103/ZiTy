using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using zity.DTOs.RejectionReasons;
using zity.Services.Interfaces;

namespace zity.Controllers
{
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
            var rejectionReason = await _rejectionReasonService.GetByIdAsync(id, includes);
            return rejectionReason == null ? NotFound() : Ok(rejectionReason);
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
            var updatedRejectionReason = await _rejectionReasonService.UpdateAsync(id, rejectionReasonUpdateDTO);
            return updatedRejectionReason == null ? NotFound() : Ok(updatedRejectionReason);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] RejectionReasonPatchDTO rejectionReasonPatchDTO)
        {
            var patchedRejectionReason = await _rejectionReasonService.PatchAsync(id, rejectionReasonPatchDTO);
            return patchedRejectionReason == null ? NotFound() : Ok(patchedRejectionReason);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _rejectionReasonService.DeleteAsync(id);
            return !result ? NotFound() : NoContent();
        }
    }
}