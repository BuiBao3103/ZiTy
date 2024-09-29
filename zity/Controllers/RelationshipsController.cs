using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using zity.DTOs.Relationships;
using zity.Services.Interfaces;

namespace zity.Controllers
{
    [Route("api/relationships")]
    [ApiController]
    public class RelationshipsController : ControllerBase
    {
        private readonly IRelationshipService _relationshipService;

        public RelationshipsController(IRelationshipService relationshipService)
        {
            _relationshipService = relationshipService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] RelationshipQueryDTO query)
        {
            return Ok(await _relationshipService.GetAllAsync(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id, [FromQuery] string? includes = null)
        {
            return Ok(await _relationshipService.GetByIdAsync(id, includes));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RelationshipCreateDTO relationshipCreateDTO)
        {
            var createdRelationship = await _relationshipService.CreateAsync(relationshipCreateDTO);
            return CreatedAtAction(nameof(Get), new { id = createdRelationship.Id }, createdRelationship);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _relationshipService.DeleteAsync(id);
            return NoContent();
        }
    }
}
