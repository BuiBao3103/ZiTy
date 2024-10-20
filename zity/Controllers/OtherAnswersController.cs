using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using zity.DTOs.OtherAnswers;
using zity.Services.Interfaces;

namespace zity.Controllers
{
    [Route("api/otherAnswers")]
    [ApiController]

    public class OtherAnswersController(IOtherAnswerService otherAnswerService) : ControllerBase
    {
        private readonly IOtherAnswerService _otherAnswerService = otherAnswerService;
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] OtherAnswerQueryDTO query)
        {
            return Ok(await _otherAnswerService.GetAllAsync(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id, [FromQuery] string? includes)
        {
            var otherAnswer = await _otherAnswerService.GetByIdAsync(id, includes);
            return otherAnswer == null ? NotFound() : Ok(otherAnswer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OtherAnswerCreateDTO otherAnswerCreateDTO)
        {
            var createdOtherAnswer = await _otherAnswerService.CreateAsync(otherAnswerCreateDTO);
            return CreatedAtAction(nameof(Get), new { id = createdOtherAnswer.Id }, createdOtherAnswer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] OtherAnswerUpdateDTO otherAnswerUpdateDTO)
        {
            var updatedOtherAnswer = await _otherAnswerService.UpdateAsync(id, otherAnswerUpdateDTO);
            return updatedOtherAnswer == null ? NotFound() : Ok(updatedOtherAnswer);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] OtherAnswerPatchDTO otherAnswerPatchDTO)
        {
            var patchedOtherAnswer = await _otherAnswerService.PatchAsync(id, otherAnswerPatchDTO);
            return patchedOtherAnswer == null ? NotFound() : Ok(patchedOtherAnswer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _otherAnswerService.DeleteAsync(id);
            return !result ? NotFound() : NoContent();
        }
    }
}