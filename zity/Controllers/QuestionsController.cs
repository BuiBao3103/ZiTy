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
            var question = await _questionService.GetByIdAsync(id, includes);
            return question == null ? NotFound() : Ok(question);
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
            var updatedQuestion = await _questionService.UpdateAsync(id, questionUpdateDTO);
            return updatedQuestion == null ? NotFound() : Ok(updatedQuestion);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] QuestionPatchDTO questionPatchDTO)
        {
            var patchedQuestion = await _questionService.PatchAsync(id, questionPatchDTO);
            return patchedQuestion == null ? NotFound() : Ok(patchedQuestion);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _questionService.DeleteAsync(id);
            return !result ? NotFound() : NoContent();
        }
    }
}
