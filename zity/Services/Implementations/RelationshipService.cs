using zity.DTOs.Relationships;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
  
    public class RelationshipService(IRelationshipRepository relationshipRepository) : IRelationshipService
    {
        private readonly IRelationshipRepository _relationshipRepository = relationshipRepository;

        public async Task<PaginatedResult<RelationshipDTO>> GetAllAsync(RelationshipQueryDTO query)
        {
            var pageRelationships = await _relationshipRepository.GetAllAsync(query);
            var RelationshipDTOs = pageRelationships.Contents.Select(RelationshipMapper.ToDTO).ToList();
            return new PaginatedResult<RelationshipDTO>(RelationshipDTOs, pageRelationships.TotalItems, pageRelationships.Page, pageRelationships.PageSize);
        }

        public async Task<RelationshipDTO> GetByIdAsync(int id, string includes)
        {
            var relationship = await _relationshipRepository.GetByIdAsync(id, includes);
            return RelationshipMapper.ToDTO(relationship);
        }

        public async Task<RelationshipDTO> CreateAsync(RelationshipCreateDTO relationshipCreateDTO)
        {
            var relationship = RelationshipMapper.ToModelFromCreate(relationshipCreateDTO);
            return RelationshipMapper.ToDTO(await _relationshipRepository.CreateAsync(relationship));
        }
    }
}
