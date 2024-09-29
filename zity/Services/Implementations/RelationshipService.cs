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

        public async Task<PaginatedResult<RelationshipDTO>> GetAllAsync(RelationshipQueryDTO queryDTO)
        {
            var pageRelationships = await _relationshipRepository.GetAllAsync(queryDTO);
            var RelationshipDTOs = pageRelationships.Contents.Select(RelationshipMapper.ToDTO).ToList();
            return new PaginatedResult<RelationshipDTO>(RelationshipDTOs, pageRelationships.TotalItems, pageRelationships.Page, pageRelationships.PageSize);
        }

        public async Task<RelationshipDTO?> GetByIdAsync(int id, string? includes)
        {
            var relationship = await _relationshipRepository.GetByIdAsync(id, includes);
            return relationship != null ? RelationshipMapper.ToDTO(relationship) : null;
        }

        public async Task<RelationshipDTO> CreateAsync(RelationshipCreateDTO createDTO)
        {
            var relationship = RelationshipMapper.ToModelFromCreate(createDTO);
            return RelationshipMapper.ToDTO(await _relationshipRepository.CreateAsync(relationship));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return  await _relationshipRepository.DeleteAsync(id);
        }
    }
}
