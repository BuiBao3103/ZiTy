﻿using Microsoft.AspNetCore.Mvc;
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
            var service = await _serviceService.GetByIdAsync(id, includes);
            return service == null ? NotFound() : Ok(service);
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
            var updatedService = await _serviceService.UpdateAsync(id, serviceUpdateDTO);
            return updatedService == null ? NotFound() : Ok(updatedService);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] ServicePatchDTO servicePatchDTO)
        {
            var patchedService = await _serviceService.PatchAsync(id, servicePatchDTO);
            return patchedService == null ? NotFound() : Ok(patchedService);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _serviceService.DeleteAsync(id);
            return !result ? NotFound() : NoContent();
        }
    }
}
