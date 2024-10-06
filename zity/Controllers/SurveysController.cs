using Microsoft.AspNetCore.Mvc;
using zity.DTOs.Surveys;
using zity.Services.Interfaces;

namespace zity.Controllers
{
    [Route("api/surveys")]
    [ApiController]
    public class SurveysController(ISurveyService surveyService) : ControllerBase
    {
        private readonly ISurveyService _surveyService = surveyService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SurveyQueryDTO query)
        {
            return Ok(await _surveyService.GetAllAsync(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id, [FromQuery] string? includes)
        {
            var survey = await _surveyService.GetByIdAsync(id, includes);
            return survey == null ? NotFound() : Ok(survey);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SurveyCreateDTO surveyCreateDTO)
        {
            var createdSurvey = await _surveyService.CreateAsync(surveyCreateDTO);
            return CreatedAtAction(nameof(Get), new { id = createdSurvey.Id }, createdSurvey);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SurveyUpdateDTO surveyUpdateDTO)
        {
            var updatedSurvey = await _surveyService.UpdateAsync(id, surveyUpdateDTO);
            return updatedSurvey == null ? NotFound() : Ok(updatedSurvey);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] SurveyPatchDTO surveyPatchDTO)
        {
            var patchedSurvey = await _surveyService.PatchAsync(id, surveyPatchDTO);
            return patchedSurvey == null ? NotFound() : Ok(patchedSurvey);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _surveyService.DeleteAsync(id);
            return !result ? NotFound() : NoContent();
        }
    }

}
