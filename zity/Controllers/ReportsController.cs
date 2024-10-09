using Microsoft.AspNetCore.Mvc;
using zity.DTOs.Reports;
using zity.Services.Interfaces;

namespace zity.Controllers
{
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
            var report = await _reportService.GetByIdAsync(id, includes);
            return report == null ? NotFound() : Ok(report);
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
            var updatedReport = await _reportService.UpdateAsync(id, reportUpdateDTO);
            return updatedReport == null ? NotFound() : Ok(updatedReport);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] ReportPatchDTO reportPatchDTO)
        {
            var patchedReport = await _reportService.PatchAsync(id, reportPatchDTO);
            return patchedReport == null ? NotFound() : Ok(patchedReport);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _reportService.DeleteAsync(id);
            return !result ? NotFound() : NoContent();
        }
    }
}
