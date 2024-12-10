using Application.DTOs.Reports;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/reports")]
[ApiController]
public class ReportsController(IReportService reportService) : ControllerBase
{
    private readonly IReportService _reportService = reportService;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ReportQueryDTO query)
    {
        return Ok(await _reportService.GetAllAsync(query));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, [FromQuery] string? includes)
    {
        return Ok(await _reportService.GetByIdAsync(id, includes));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReportCreateDTO reportCreateDTO)
    {
        var createdReport = await _reportService.CreateAsync(reportCreateDTO);
        return CreatedAtAction(nameof(Get), new { id = createdReport.Id }, createdReport);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ReportUpdateDTO reportUpdateDTO)
    {
        return Ok(await _reportService.UpdateAsync(id, reportUpdateDTO));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] ReportPatchDTO reportPatchDTO)
    {
        return Ok(await _reportService.PatchAsync(id, reportPatchDTO));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _reportService.DeleteAsync(id);
        return NoContent();
    }
}
