using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using zity.DTOs.UserAnswers;
using zity.Services.Interfaces;

namespace zity.Controllers
{
    [Route("api/userAnswers")]
    [ApiController]

    public class UserAnswersController(IUserAnswerService userAnswerService) : ControllerBase
    {
        private readonly IUserAnswerService _userAnswerService = userAnswerService;
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] UserAnswerQueryDTO query)
        {
            return Ok(await _userAnswerService.GetAllAsync(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id, [FromQuery] string? includes)
        {
            var userAnswer = await _userAnswerService.GetByIdAsync(id, includes);
            return userAnswer == null ? NotFound() : Ok(userAnswer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserAnswerCreateDTO userAnswerCreateDTO)
        {
            var createdUserAnswer = await _userAnswerService.CreateAsync(userAnswerCreateDTO);
            return CreatedAtAction(nameof(Get), new { id = createdUserAnswer.Id }, createdUserAnswer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserAnswerUpdateDTO userAnswerUpdateDTO)
        {
            var updatedUserAnswer = await _userAnswerService.UpdateAsync(id, userAnswerUpdateDTO);
            return updatedUserAnswer == null ? NotFound() : Ok(updatedUserAnswer);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] UserAnswerPatchDTO userAnswerPatchDTO)
        {
            var patchedUserAnswer = await _userAnswerService.PatchAsync(id, userAnswerPatchDTO);
            return patchedUserAnswer == null ? NotFound() : Ok(patchedUserAnswer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _userAnswerService.DeleteAsync(id);
            return !result ? NotFound() : NoContent();
        }
    }
}