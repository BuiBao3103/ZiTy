using Application.DTOs.OtherAnswers;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers;

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
        return Ok(await _otherAnswerService.GetByIdAsync(id, includes));
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
        return Ok(await _otherAnswerService.UpdateAsync(id, otherAnswerUpdateDTO));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] OtherAnswerPatchDTO otherAnswerPatchDTO)
    {
        return Ok(await _otherAnswerService.PatchAsync(id, otherAnswerPatchDTO));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _otherAnswerService.DeleteAsync(id);
        return NoContent();
    }
}