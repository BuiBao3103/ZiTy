using Microsoft.AspNetCore.Mvc;
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
    }
}
