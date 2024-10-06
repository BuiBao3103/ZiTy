using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
            var answer = await _answerService.GetByIdAsync(id, includes);
            return answer == null ? NotFound() : Ok(answer);
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
            var updatedAnswer = await _answerService.UpdateAsync(id, answerUpdateDTO);
            return updatedAnswer == null ? NotFound() : Ok(updatedAnswer);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] AnswerPatchDTO answerPatchDTO)
        {
            var patchedAnswer = await _answerService.PatchAsync(id, answerPatchDTO);
            return patchedAnswer == null ? NotFound() : Ok(patchedAnswer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _answerService.DeleteAsync(id);
            return !result ? NotFound() : NoContent();
        }
    }
}