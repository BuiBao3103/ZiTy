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
            return Ok(await _userAnswerService.GetByIdAsync(id, includes));
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
            return Ok(await _userAnswerService.UpdateAsync(id, userAnswerUpdateDTO));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] UserAnswerPatchDTO userAnswerPatchDTO)
        {
            return Ok(await _userAnswerService.PatchAsync(id, userAnswerPatchDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _userAnswerService.DeleteAsync(id);
            return NoContent();
        }
    }
}