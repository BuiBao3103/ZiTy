using Microsoft.AspNetCore.Mvc;
using zity.DTOs.Services;
using zity.Services.Interfaces;

namespace zity.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServicesController(IServiceService serviceService) : ControllerBase
    {
        private readonly IServiceService _serviceService = serviceService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ServiceQueryDTO query)
        {
            return Ok(await _serviceService.GetAllAsync(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id, [FromQuery] string? includes)
        {
            return Ok(await _serviceService.GetByIdAsync(id, includes));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceCreateDTO serviceCreateDTO)
        {
            var createdService = await _serviceService.CreateAsync(serviceCreateDTO);
            return CreatedAtAction(nameof(Get), new { id = createdService.Id }, createdService);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ServiceUpdateDTO serviceUpdateDTO)
        {
            return Ok(await _serviceService.UpdateAsync(id, serviceUpdateDTO));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] ServicePatchDTO servicePatchDTO)
        {
            return Ok(await _serviceService.PatchAsync(id, servicePatchDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _serviceService.DeleteAsync(id);
            return NoContent();
        }
    }
}
