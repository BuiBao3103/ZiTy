using Application.DTOs;
using Application.DTOs.Surveys;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

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
        return Ok(await _surveyService.GetByIdAsync(id, includes));
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
        return Ok(await _surveyService.UpdateAsync(id, surveyUpdateDTO));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] SurveyPatchDTO surveyPatchDTO)
    {
        return Ok(await _surveyService.PatchAsync(id, surveyPatchDTO));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _surveyService.DeleteAsync(id);
        return NoContent();
    }
}
