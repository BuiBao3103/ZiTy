using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using zity.DTOs.Questions;
using zity.Services.Interfaces;
namespace zity.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionsController(IQuestionService questionService) : ControllerBase
    {
        private readonly IQuestionService _questionService = questionService;
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QuestionQueryDTO query)
        {
            return Ok(await _questionService.GetAllAsync(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id, [FromQuery] string? includes)
        {
            return Ok(await _questionService.GetByIdAsync(id, includes));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuestionCreateDTO questionCreateDTO)
        {
            var createdQuestion = await _questionService.CreateAsync(questionCreateDTO);
            return CreatedAtAction(nameof(Get), new { id = createdQuestion.Id }, createdQuestion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] QuestionUpdateDTO questionUpdateDTO)
        {
            return Ok(await _questionService.UpdateAsync(id, questionUpdateDTO));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] QuestionPatchDTO questionPatchDTO)
        {
            return Ok(await _questionService.PatchAsync(id, questionPatchDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _questionService.DeleteAsync(id);
            return NoContent();
        }
    }
}
