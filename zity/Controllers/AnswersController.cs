using Microsoft.AspNetCore.Mvc;
using zity.DTOs.Answers;
using zity.Services.Interfaces;

namespace zity.Controllers
{
    [Route("api/answers")]
    [ApiController]

    public class AnswersController(IAnswerService answerService) : ControllerBase
    {
        private readonly IAnswerService _answerService = answerService;
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AnswerQueryDTO query)
        {
            return Ok(await _answerService.GetAllAsync(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id, [FromQuery] string? includes)
        {
            return Ok(await _answerService.GetByIdAsync(id, includes));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AnswerCreateDTO answerCreateDTO)
        {
            var createdAnswer = await _answerService.CreateAsync(answerCreateDTO);
            return CreatedAtAction(nameof(Get), new { id = createdAnswer.Id }, createdAnswer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AnswerUpdateDTO answerUpdateDTO)
        {
            return Ok(await _answerService.UpdateAsync(id, answerUpdateDTO));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] AnswerPatchDTO answerPatchDTO)
        {
            return Ok(await _answerService.PatchAsync(id, answerPatchDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _answerService.DeleteAsync(id);
            return NoContent();
        }
    }
}